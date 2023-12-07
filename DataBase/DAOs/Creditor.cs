namespace KraviaTest.DataBase.DAOs
{
    public class Creditor
    {
        public int Id { get; set; }
        public string OrganisationNumber { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }

        public Creditor(int id, string organisationNumber, string name, string email, string address, string city, string country, string postalCode, string phone)
        {
            Id = id;
            OrganisationNumber = organisationNumber;
            Name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.country = country;
            this.postalCode = postalCode;
            this.phone = phone;
        }
    }
}
