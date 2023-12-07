using KraviaTest.DataBase.DAOs;

namespace KraviaTest.DataBase
{
    public interface IDbContext
    {
        public List<Notification> GetNotifications(int user, bool includeReaded);
        bool AddNotification(Notification notification);
        public List<Bill> GetBills(int creditorID, bool OnlyUnpaid);
        public List<Creditor> GetCreditors();
        public List<Debtor> GetDebtors();
        int GetCreditorID(string creditorIDNumber);
        void SetDebtorInvalidAddress(int id);
        void SetNotificationsReaded(List<int> id);
        List<Debtor> GetDebtors(int creditorID);
    }
}
