namespace BankingApp.Models.Views
{
    public class TransferVM
    {
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        public int ToAccountId { get; set; }
        public decimal ToAccountBalance { get; set; }
        public DateTime TransactionTime { get; set; }

        public decimal Amount { get; set; }
        
    }
}
