using KraviaTest.DataBase;
using KraviaTest.DataBase.DAOs;
using KraviaTest.Events;
using KraviaTest.Handlers.Interfaces;
using KraviaTest.Models;
using KraviaTest.Services.Interfaces;
using KraviaTest.Templates;

namespace KraviaTest.Handlers.Implementations
{
    public class PostenFailureHandler(IEmailService _IEmailService, IDbContext _IDbContext) : IPostenFailureHandler
    {
        private readonly IEmailService iEmailService = _IEmailService;
        private readonly IDbContext iDbContext = _IDbContext;
        private const string NOTIFICATION_TYPE_INVALID_ADDRESS = "Invalid_Debtor_Address";

        public void HandleFailure(PostenFailureInfoModel data)
        {
            SendEmail(data);
            SendNotification(data);
            UpdateDebtorInDB(data);
        }

        private void UpdateDebtorInDB(PostenFailureInfoModel data)
        {
            Debtor debtor;
            //Getting debtor by address and name
            if(String.IsNullOrEmpty(data.DebtorIDNumber))
                debtor = iDbContext.GetDebtors().FirstOrDefault(x => x.address == data.DebtorAddress && x.Name == data.DebtorName);
            else
                debtor = iDbContext.GetDebtors().FirstOrDefault(x => x.IdNumber == data.DebtorIDNumber);

            if(debtor != null) { 
                Console.WriteLine($"Updating DB. {debtor.Name} validAddress OLD = {debtor.validAddress}");

                iDbContext.SetDebtorInvalidAddress(debtor.Id);

                Console.WriteLine("New value: " + iDbContext.GetDebtors().FirstOrDefault(x => x.Id == debtor.Id).validAddress);
            }
            else
                Console.WriteLine("ALERT: Debtor not found");
        }

        private void SendNotification(PostenFailureInfoModel data)
        {
            Notification notif = new Notification();
            notif.ToUser = iDbContext.GetCreditorID(data.CreditorIDNumber);
            notif.Type = NOTIFICATION_TYPE_INVALID_ADDRESS;
            notif.Content = $"Debtor named {data.DebtorName} with ID Number {data.DebtorIDNumber} has invalid address.";

            EventManager.Instance.PublishEvent(EventType.WebNotification, notif);
        }

        public void SendEmail(PostenFailureInfoModel data)
        {
            EmailDataModel emailData = new EmailDataModel
            {
                From = "kravia@kravia.com",
                To = new List<string> { data.CreditorEmail },
                Subject = "Debtor has incorrect address"
            };

            Dictionary<string, object?> renderParameters = new Dictionary<string, object?>();
            renderParameters.Add("CreditorName", data.CreditorName);
            renderParameters.Add("DebtorName", data.DebtorName);
            renderParameters.Add("DebtorAddress", data.DebtorAddress);

            emailData.Body = Utilities.RenderEmail<FailedDelivery>(renderParameters);
            iEmailService.SendEmail(emailData);
        }
    }
}
