using KraviaTest.Models;
using Org.BouncyCastle.Asn1.Pkcs;

namespace KraviaTest.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(EmailDataModel mailData);
    }
}
