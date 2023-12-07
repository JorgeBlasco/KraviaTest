using Digipost.Api.Client.Common;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace KraviaTest.Models
{
    public class MailDataModel
    {
        public SenderDataModel Sender {  get; set; }
        public RecipientDataModel Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SenderDataModel
    {
        public string Name { get; set; }
        public string OrganisationNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalAddress1 { get; set; }
        public string? PostalAddress2 { get; set; }
        public string Email { get; set; }
    }

    public class RecipientDataModel
    {
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalAddress1 { get; set; }
        public string? PostalAddress2 { get; set; }
        public string Email { get; set; }
        public string? DigipostAddress { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
