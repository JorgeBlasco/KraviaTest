using Digipost.Api.Client.Common;
using Digipost.Api.Client;
using Digipost.Api.Client.Common.Print;
using Digipost.Api.Client.Send;
using KraviaTest.Models;
using Microsoft.AspNetCore.Mvc;
using Digipost.Api.Client.DataTypes.Core.Internal;
using KraviaTest.Events;
using KraviaTest.Handlers.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KraviaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private IPostenFailureHandler _failureHandler;
        public SendMailController(IPostenFailureHandler failureHandler) {
            _failureHandler = failureHandler;
        }

        [HttpPost]
        public void Post(MailDataModel data)
        {
            //Mockup Data
            var broker = new Broker(12345);
            var sender = new Digipost.Api.Client.Common.Sender(67890);
            var clientConfig = new ClientConfig(broker, Digipost.Api.Client.Common.Environment.Test);
            var client = new DigipostClient(clientConfig, Utilities.GetCert());
            /////////////

            var printDetails =
                new PrintDetails(
                    printRecipient: new PrintRecipient(
                        data.Recipient.Name,
                        new NorwegianAddress(data.Recipient.PostalCode, data.Recipient.City, data.Recipient.PostalAddress1, data.Recipient.PostalAddress2)),
                    printReturnRecipient: new PrintReturnRecipient(
                        data.Sender.Name,
                        new NorwegianAddress(data.Sender.PostalCode, data.Sender.City, data.Sender.PostalAddress1, data.Sender.PostalAddress2))
                );

            //var primaryDocument = new Document(data.Subject, data.Attachment.MimeType, data.Attachment.Bytes);
            var primaryDocument = new Document(data.Subject, "test", new byte[2]);

            IMessageDeliveryResult result = new MessageDeliveryResult();
            try
            {
                var messageToPrint = new PrintMessage(new Digipost.Api.Client.Common.Sender(12345), printDetails, primaryDocument);

                result = client.SendMessage(messageToPrint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (result.Status == Digipost.Api.Client.Common.Enums.MessageStatus.NotComplete) {
                PostenFailureInfoModel failureInfo = new PostenFailureInfoModel();
                failureInfo.DebtorAddress = data.Recipient.PostalAddress1;
                failureInfo.DebtorName = data.Recipient.Name;
                failureInfo.DebtorTlf = data.Recipient.Phone;
                failureInfo.DebtorIDNumber = data.Recipient.IdNumber;
                failureInfo.CreditorEmail = data.Sender.Email;
                failureInfo.CreditorName = data.Sender.Name;
                failureInfo.CreditorIDNumber = data.Sender.OrganisationNumber;

                EventManager.Instance.PublishEvent(EventType.POSTENMailDeliveryFail, failureInfo);
            }
        }
    }
}
