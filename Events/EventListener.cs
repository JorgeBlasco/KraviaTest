using KraviaTest.Controllers;
using KraviaTest.Handlers.Interfaces;
using KraviaTest.Models;
using KraviaTest.Services.Interfaces;

namespace KraviaTest.Events
{
    public class EventListener : BackgroundService
    {
        private readonly IEventHandler _IEventHandler;
        public EventListener(IEventHandler iEventHandler) {
            _IEventHandler = iEventHandler;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventManager.Instance.OnEventPublishedEvent += _IEventHandler.OnEventReceived;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

/*        static void OnEventPublished(object sender, MessageEventArgs e)
        {
            switch (e.MessageTopic)
            {
                case EventType.POSTENMailDeliveryFail:
                case EventType.POSTENMailReturned:

                    //PostenWebHookController (PostenEventModel)
                    //SendMailController (MailDataModel)
                    //TODO Revisar que los objetos sean iguales

                    break;
                case EventType.WebNotification:
                    Utilities.SendNotification(e.Data);
                    break;
                default:
                    break;
            };
        }*/
    }
}
