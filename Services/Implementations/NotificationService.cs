using KraviaTest.DataBase;
using KraviaTest.DataBase.DAOs;
using KraviaTest.Services.Interfaces;

namespace KraviaTest.Services.Implementations
{
    public class NotificationService (IDbContext _IDbContext) : INotificationService
    {
        private readonly IDbContext iDbContext = _IDbContext;

        public List<Notification> GetNotifications(int creditorID, bool includeReaded = false)
        {
            return iDbContext.GetNotifications(creditorID, includeReaded);
        }

        public bool SendNotification(Notification notif)
        {
            return iDbContext.AddNotification(notif);
        }

        public void SetNotificationsReaded(List<Notification> notifications)
        {
            iDbContext.SetNotificationsReaded(notifications.Select(x => x.ID).ToList());
        }
    }
}
