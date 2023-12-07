namespace KraviaTest.Events
{
    public class EventManager
    {
        public event EventHandler<MessageEventArgs> OnEventPublishedEvent;

        private static readonly EventManager _instance = new EventManager();

        private EventManager(){}

        public static EventManager Instance { get { return _instance; } }
        public void PublishEvent(EventType messageTopic, object? data)
        {
            MessageEventArgs args = new()
            {
                MessageTopic = messageTopic,
                Data = data
            };
            OnEventPublished(args);
        }

        protected virtual void OnEventPublished(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = OnEventPublishedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
