using KraviaTest.DataBase.DAOs;

namespace KraviaTest.Services.Interfaces
{
    public interface INotificationService
    {
        List<Notification> GetNotifications(int creditorID, bool includeReaded = false);
        public bool SendNotification(Notification data);
        void SetNotificationsReaded(List<Notification> notifications);
    }
}
