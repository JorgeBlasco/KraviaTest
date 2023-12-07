using KraviaTest.DataBase;
using KraviaTest.DataBase.DAOs;
using KraviaTest.Models;
using KraviaTest.Services.Interfaces;
using System.Text;

namespace KraviaTest.Services.Implementations
{
    public class DashboardService (IDbContext _IDbContext, INotificationService _iNotificationService) : IDashboardService
    {
        private readonly IDbContext iDbContext = _IDbContext;
        private readonly INotificationService iNotificationService = _iNotificationService;
        public string GetDashboardData(int creditorID)
        {
            //Dashboard info
            List<Debtor> debtors = iDbContext.GetDebtors(creditorID);
            List<Debtor> invalidDebtors = debtors.Where(x => x.validAddress == false).ToList();
            List<Bill> bills = iDbContext.GetBills(creditorID, true);
            int numDebtors = debtors.Count;
            int numInvalidDebtors = invalidDebtors.Count();
            int numBills = bills.Count;
            decimal totalDebt = bills.Sum(x => x.Amount);
            decimal totalInvalidDebt = bills.Where(x=> invalidDebtors.Select(d => d.Id).Contains(x.Id)).Sum(x => x.Amount);

            DashboardDataModel dashData = new DashboardDataModel();
            dashData.numDebtors = numDebtors;
            dashData.numInvalidDebtors = numInvalidDebtors;
            dashData.numBills = numBills;
            dashData.totalDebt = totalDebt;
            dashData.totalInvalidDebt = totalInvalidDebt;

            //Notifications Info
            List<Notification> notifications = iNotificationService.GetNotifications(creditorID, false);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\nList of notifications:");
            if (notifications.Count == 0)
                sb.AppendLine("No notifications\n");
            else
                foreach (Notification notification in notifications)
                {
                    sb.AppendLine($"Notification no {notification.ID}, \"{notification.Type}\"");
                    sb.AppendLine($"Content: {notification.Content}\n");
                }
            iNotificationService.SetNotificationsReaded(notifications);

            sb.Append(dashData.ToString());

            return  sb.ToString();
        }
    }
}
