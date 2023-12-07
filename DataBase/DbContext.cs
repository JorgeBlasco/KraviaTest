using KraviaTest.DataBase.DAOs;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Asn1.X509.Qualified;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KraviaTest.DataBase
{
    public class DbContext : IDbContext
    {
        private List<Notification> notifications;
        private List<Bill> bills;
        private List<Creditor> creditors;
        private List<Debtor> debtors;

        public DbContext()
        {
            creditors = new List<Creditor>();
            debtors = new List<Debtor>();
            notifications = new List<Notification>();
            bills = new List<Bill>();

            #region Sample data
            creditors.Add(new Creditor(1, "987654321", "Creditor1","email1@creditor.com","Address 1","City1","Country","1234","12345678"));
            creditors.Add(new Creditor(2, "987654322", "Creditor2", "email2@creditor.com", "Address 2","City2","Country","2234","22345678"));
            creditors.Add(new Creditor(3, "987654323", "Creditor3", "email3@creditor.com", "Address 3","City3","Country","3234","32345678"));
            creditors.Add(new Creditor(4, "987654324", "Creditor4", "email4@creditor.com", "Address 4","City4","Country","4234","42345678"));
            creditors.Add(new Creditor(5, "987654325", "Creditor5", "email5@creditor.com", "Address 5","City5","Country","5234","52345678"));
            
            debtors.Add(new Debtor(1, "987654311", "Debtor1", "email1@debtor.com", "Addressen 1", "By1", "Country", "1234", "11234567", true));
            debtors.Add(new Debtor(2, "987654312", "Debtor2", "email2@debtor.com", "Addressen 2", "By2", "Country", "2234", "21234567", true));
            debtors.Add(new Debtor(3, "987654313", "Debtor3", "email3@debtor.com", "Addressen 3", "By3", "Country", "3234", "31234567", true));
            debtors.Add(new Debtor(4, "987654314", "Debtor4", "email4@debtor.com", "Addressen 4", "By4", "Country", "4234", "41234567", true));
            debtors.Add(new Debtor(5, "987654315", "Debtor5", "email5@debtor.com", "Addressen 5", "By5", "Country", "5234", "51234567", true));
            debtors.Add(new Debtor(6, "987654316", "Debtor6", "email6@debtor.com", "Addressen 6", "By6", "Country", "6234", "61234567", false));
            debtors.Add(new Debtor(7, "987654317", "Debtor7", "email7@debtor.com", "Addressen 7", "By7", "Country", "7234", "71234567", true));
            debtors.Add(new Debtor(8, "987654318", "Debtor8", "email8@debtor.com", "Addressen 8", "By8", "Country", "8234", "81234567", false));
            debtors.Add(new Debtor(9, "987654319", "Debtor9", "email9@debtor.com", "Addressen 9", "By9", "Country", "9234", "91234567", true));
            
            notifications.Add(new Notification(1, 1, "Invalid_Debtor_Address", "Lorem Ipsum"));
            notifications.Add(new Notification(2, 2, "Invalid_Debtor_Address", "Lorem Ipsum"));
            notifications.Add(new Notification(3, 3, "Invalid_Debtor_Address", "Lorem Ipsum"));
            notifications.Add(new Notification(4, 4, "Invalid_Debtor_Address", "Lorem Ipsum"));
            notifications.Add(new Notification(5, 5, "Invalid_Debtor_Address", "Lorem Ipsum"));
            
            bills.Add(new Bill(1,1,1,1000.05m,true));
            bills.Add(new Bill(2,1,1,1230.75m,true));
            bills.Add(new Bill(3,1,1,3456.12m, true));
            bills.Add(new Bill(4,1,1,990.05m, true));
            bills.Add(new Bill(5,1,1,10035m,true));
            bills.Add(new Bill(6,2,1,720m,true));
            bills.Add(new Bill(7,2,1,1010.15m,false));
            bills.Add(new Bill(8,2,1,2020.25m, true));
            bills.Add(new Bill(9,2,1,3030.35m,false));
            bills.Add(new Bill(10,2,1,4400.45m, true));
            bills.Add(new Bill(11,2,1,5500.55m,false));

            bills.Add(new Bill(12,3,1,1500.99m, true));
            bills.Add(new Bill(13,3,1,2500.01m,false));
            bills.Add(new Bill(14,3,1,3500.03m, true));
            bills.Add(new Bill(15,3,1,4500.34m,false));
            bills.Add(new Bill(16,3,1,2500.23m, true));
            bills.Add(new Bill(17,4,1,5500.63m,false));
            bills.Add(new Bill(18,4,1,2500.21m, true));
            bills.Add(new Bill(19,4,1,3500.5m,false));
            bills.Add(new Bill(20,4,1,2500m, true));
            bills.Add(new Bill(21,4,1,1500.05m,false));

            bills.Add(new Bill(22,5,1,100.55m, true));
            bills.Add(new Bill(23,5,1,200.55m,false));
            bills.Add(new Bill(24,5,1,200.55m, true));
            bills.Add(new Bill(25,5,1,500.55m,false));
            bills.Add(new Bill(26,5,1,500.55m, true));
            bills.Add(new Bill(27,6,1,550.55m,false));
            bills.Add(new Bill(28,6,1,50.55m, true));
            bills.Add(new Bill(29,6,1,55.55m,false));

            bills.Add(new Bill(30,6,1,10.55m, true));
            bills.Add(new Bill(31,6,1,5.55m,false));
            bills.Add(new Bill(32,6,1,1.55m, true));
            bills.Add(new Bill(33,7,1,52.55m,false));
            bills.Add(new Bill(34,7,1,55.55m, true));
            bills.Add(new Bill(35,7,1,50.55m,false));
            bills.Add(new Bill(36,7,1,500.55m, true));
            bills.Add(new Bill(37,7,1,5.55m,false));
            bills.Add(new Bill(38,8,1,50.55m, true));
            
            bills.Add(new Bill(39,8,1,1010.55m, true));
            bills.Add(new Bill(40,8,1,1020.55m,false));
            bills.Add(new Bill(41,8,1,1030.55m, true));
            bills.Add(new Bill(42,8,1,1040.55m,false));
            bills.Add(new Bill(43,8,1,1050.55m, true));
            bills.Add(new Bill(44,9,1,1060.55m,false));
            bills.Add(new Bill(45,9,1,1070.55m, true));
            bills.Add(new Bill(46,9,1,1080.55m,false));
            bills.Add(new Bill(47,9,1,1090.55m, true));
            bills.Add(new Bill(48,9,1,1000.55m,false));



            #endregion
        }

        public List<Notification> GetNotifications(int user, bool includeReaded = false)
        {
            List<Notification> db = notifications.FindAll(x => x.IsReaded == includeReaded && x.ToUser == user);
            List<Notification> list = new List<Notification>(db);
            return list;
        }

        public bool AddNotification(Notification notification)
        {
            int newId = notifications.Max(x => x.ID) + 1;
            Notification notif = new Notification(newId,notification.ToUser,notification.Type, notification.Content);
            Console.WriteLine($"Notification generated: Id: {newId} Type: {notif.Type}, Message: {notif.Content}");
            notifications.Add(notif);
            return true;
        }

        public List<Bill> GetBills(int creditorId, bool OnlyUnpaid)
        {
            return bills.FindAll(x => x.Unpaid == OnlyUnpaid && x.CreditorID == creditorId);
        }

        public List<Creditor> GetCreditors()
        {
            return creditors;
        }

        public List<Debtor> GetDebtors()
        {
            List<Debtor> list = new List<Debtor>(debtors);
            return list;
        }

        public int GetCreditorID(string creditorIDNumber)
        {
            return creditors.FirstOrDefault(x => x.OrganisationNumber == creditorIDNumber).Id;
        }

        public void SetDebtorInvalidAddress(int id)
        {
            int index = debtors.Select((debtor,index) => (debtor,index)).FirstOrDefault(x => x.debtor.Id==id).index;
            Debtor deb = debtors[index];
            deb.validAddress = false;
            debtors[index] = deb;
        }

        public void SetNotificationsReaded(List<int> idList)
        {foreach (int id in idList)
            {
                int index = notifications.Select((notification, index) => (notification, index)).FirstOrDefault(x => x.notification.ID == id).index;
                Notification not = notifications[index];
                not.IsReaded = true;
                notifications[index] = not;
            }
        }

        public List<Debtor> GetDebtors(int creditorID)
        {
            var _bills = GetBills(creditorID, true);
            var _debtors = from b in _bills
                           join d in debtors
                           on b.DebtorID equals d.Id
                           select d;

            return _debtors.Distinct().ToList();
        }
    }
}
