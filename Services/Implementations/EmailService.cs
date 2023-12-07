using KraviaTest.Models;
using KraviaTest.Services.Interfaces;
using MimeKit;

namespace KraviaTest.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(EmailDataModel mailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailData.From, mailData.From));
            foreach (var adress in mailData.To) {
                message.To.Add(new MailboxAddress(adress,adress));
            }
            message.Subject = mailData.Subject;
            message.Body = new TextPart("html") { Text = mailData.Body};
            string name = Guid.NewGuid().ToString();

            try
            {
                Directory.CreateDirectory("MailStorage");
                message.WriteTo("MailStorage//" + name + ".eml");
                Console.WriteLine("Generated Mail: " + name);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
