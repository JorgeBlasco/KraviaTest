using System.Collections.Generic;

namespace KraviaTest.Models
{
    public class EmailDataModel
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string>? Cc { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
