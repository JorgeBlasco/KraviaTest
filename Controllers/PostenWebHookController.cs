using Digipost.Api.Client.Common.Enums;
using KraviaTest.Events;
using KraviaTest.Handlers.Interfaces;
using KraviaTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace KraviaTest.Controllers
{
    [Route("api/posten")]
    [ApiController]
    public class PostenWebHookController : ControllerBase
    {
        [HttpPost]
        public void ReceiveEvent(PostenEventModel postenEvent)
        {
            if (postenEvent.EventType == Enum.GetName(typeof(DocumentEventType), DocumentEventType.PrintFailed) ||
                postenEvent.EventType == Enum.GetName(typeof(DocumentEventType), DocumentEventType.Shredded))
            {
                PostenFailureInfoModel failureInfo = new PostenFailureInfoModel();
                failureInfo.DebtorAddress = postenEvent.Mail.Recipient.PostalAddress1;
                failureInfo.DebtorName = postenEvent.Mail.Recipient.Name;
                failureInfo.DebtorTlf = postenEvent.Mail.Recipient.Phone;
                failureInfo.CreditorEmail = postenEvent.Mail.Sender.Email;
                failureInfo.CreditorName = postenEvent.Mail.Sender.Name;
                failureInfo.CreditorIDNumber = postenEvent.Mail.Sender.OrganisationNumber;

                EventManager.Instance.PublishEvent(EventType.POSTENMailReturned, failureInfo);
            }
        }
    }
}
