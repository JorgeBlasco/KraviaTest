namespace KraviaTest.DataBase.DAOs
{
    public class Bill
    {
        public int Id { get; set; }
        public int DebtorID { get; set; }
        public int CreditorID { get; set; }
        public decimal Amount { get; set; }
        public bool Unpaid { get; set; }

        public Bill(int id, int debtorID, int creditorID, decimal amount, bool unpaid)
        {
            Id = id;
            DebtorID = debtorID;
            CreditorID = creditorID;
            Amount = amount;
            Unpaid = unpaid;
        }
    }
}
