namespace BankingApp.Models.View
{
    public class TransactionVM
    {
        public int Id { get; set; }
        public int FromAccountId { get; set; }

        public string FromAccount { get; set; }
        public decimal FromAccountBalance { get; set; }
        public int ToAccountId { get; set; }
        public string ToAccount { get; set; }
        public decimal ToAccountBalance { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal Amount { get; set; }

    }
}