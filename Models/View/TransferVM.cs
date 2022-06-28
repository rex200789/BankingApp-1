using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models.Views
{
    public class TransferVM
    {
        [Required]
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        [Required]
        public int ToAccountId { get; set; }
        public decimal ToAccountBalance { get; set; }
        public DateTime TransactionTime { get; set; }
        [Required]
        public decimal Amount { get; set; }
        
    }
}
