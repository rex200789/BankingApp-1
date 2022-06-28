using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Transaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Display(Name = "From Account")]
        public int FromAccountId { get; set; }
        [Display(Name = "From Account Balance")]
        public decimal FromAccountBalance { get; set; }
        [Display(Name = "To Account")]
        public int ToAccountId { get; set; }
        [Display(Name = "To Account Balance")]
        public decimal ToAccountBalance { get; set; }
        [Display(Name = "Transaction Time")]
        public DateTime TransactionTime { get; set; }
        [Display(Name = "Amount Debited")]
        public decimal Amount { get; set; }

        [ForeignKey("FromAccountId")]
        public virtual Account FromAccountG { get; set; }
        [ForeignKey("ToAccountId")]
        public virtual Account ToAccountG { get; set; }
    }
}
