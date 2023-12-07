namespace KraviaTest.DataBase.DAOs
{
    public class Debtor
    {
        public int Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public bool validAddress { get; set; }

        public Debtor(int id, string idNumber, string name, string email, string address, string city, string country, string postalCode, string phone, bool validAddress)
        {
            Id = id;
            IdNumber = idNumber;
            Name = name;
            this.email = email;
            this.address = address;
            this.city = city;
            this.country = country;
            this.postalCode = postalCode;
            this.phone = phone;
            this.validAddress = validAddress;
        }
    }
}
