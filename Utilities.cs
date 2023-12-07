using Digipost.Api.Client.Send;
using KraviaTest.Models;
using KraviaTest.Templates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace KraviaTest
{
    public static class Utilities
    {
        public static X509Certificate2 GetCert()
        {
            SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
            sanBuilder.AddIpAddress(IPAddress.Loopback);
            sanBuilder.AddIpAddress(IPAddress.IPv6Loopback);
            sanBuilder.AddDnsName("localhost");
            sanBuilder.AddDnsName(Environment.MachineName);

            X500DistinguishedName distinguishedName = new X500DistinguishedName($"CN=cert");

            using (RSA rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(distinguishedName, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));


                request.CertificateExtensions.Add(
                   new X509EnhancedKeyUsageExtension(
                       new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));

                request.CertificateExtensions.Add(sanBuilder.Build());

                var certificate = request.CreateSelfSigned(new DateTimeOffset(DateTime.UtcNow.AddDays(-1)), new DateTimeOffset(DateTime.UtcNow.AddDays(3650)));
                certificate.FriendlyName = "cert";

                return new X509Certificate2(certificate.Export(X509ContentType.Pfx, "pass"), "pass", X509KeyStorageFlags.MachineKeySet);
            }
        }

        public static string RenderEmail<TComponent> (Dictionary<string, object?> renderParameters) where TComponent : IComponent
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

            var parameters = ParameterView.FromDictionary(renderParameters);

            var html = htmlRenderer.Dispatcher.InvokeAsync( () =>
            {
                var output = htmlRenderer.BeginRenderingComponent<TComponent> (parameters);

                return output.ToHtmlString();
            });
            return html.Result;
        }

        public static bool SendNotification(Object data)
        {
            return false;
        }
    }  
}
