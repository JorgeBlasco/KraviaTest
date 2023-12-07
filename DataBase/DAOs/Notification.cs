namespace KraviaTest.DataBase.DAOs
{
    public class Notification
    {
        public int ID { get; set; }
        public int ToUser { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public bool IsReaded { get; set; }
        public DateTime DateTime { get; set; }

        public Notification() { }
        public Notification(int id, int toUser, string type, string content)
        {
            ID = id;
            ToUser = toUser;
            Type = type;
            Content = content;
            IsReaded = false;
            DateTime = DateTime.Now;
        }
    }
}
