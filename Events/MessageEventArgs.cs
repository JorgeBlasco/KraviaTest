namespace KraviaTest.Events
{
    public class MessageEventArgs : EventArgs
    {
        public EventType MessageTopic { get; set; }
        public object Data { get; set; }
    }
}
