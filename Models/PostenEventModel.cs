namespace KraviaTest.Models
{
    public class PostenEventModel
    {
        public int UUID { get; set; }
        public string EventType { get; set; }
        public string Metadata { get; set; }
        public DateTime Created { get; set; }
        public MailDataModel Mail { get; set; }
    }
}
