using System.Text;

namespace KraviaTest.Models
{
    public class DashboardDataModel
    {
        public int numDebtors { get; set; }
        public int numInvalidDebtors { get; set; }
        public int numBills { get; set; }
        public decimal totalDebt { get; set; }
        public decimal totalInvalidDebt { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\n");
            sb.AppendLine($"Total number of debtors is {numDebtors}.");
            sb.AppendLine($"There are a total of {numBills} bills pending. Total debt pending to collect is {totalDebt}.");
            sb.AppendLine($"There are {numInvalidDebtors} debtors with INVALID addresses.");
            sb.AppendLine($"A correct address must be provided in order to make possible debt collection.");
            sb.AppendLine($"Invalid debtors total debt is: {totalInvalidDebt}");
            sb.AppendLine("\n\n");
            return sb.ToString();
        }
    }
}
