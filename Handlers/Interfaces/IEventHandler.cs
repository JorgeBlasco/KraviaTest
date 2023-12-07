using KraviaTest.Events;

namespace KraviaTest.Handlers.Interfaces
{
    public interface IEventHandler
    {
        public void OnEventReceived(object sender, MessageEventArgs e);
    }
}
