using KraviaTest.DataBase.DAOs;
using KraviaTest.Events;
using KraviaTest.Handlers.Interfaces;
using KraviaTest.Models;
using KraviaTest.Services.Interfaces;

namespace KraviaTest.Handlers.Implementations
{
    public class EventHandler : IEventHandler
    {
        private readonly IPostenFailureHandler postenFailureHandler;
        private readonly INotificationService notificationService;
        public EventHandler(IPostenFailureHandler _postenFailureHandler, INotificationService _notificationService)
        {
            postenFailureHandler = _postenFailureHandler;
            notificationService = _notificationService;
        }

        public void OnEventReceived(object sender, MessageEventArgs e)
        {
            switch (e.MessageTopic)
            {
                case EventType.POSTENMailDeliveryFail:
                case EventType.POSTENMailReturned:
                    postenFailureHandler.HandleFailure((PostenFailureInfoModel)e.Data);
                    break;
                case EventType.WebNotification:
                    notificationService.SendNotification((Notification)e.Data);
                    break;
                default:
                    break;
            };
        }
    }
}
