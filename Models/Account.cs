using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Account
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set ; }
        [Required]
        [Display(Name = "AccountName")]
        public string Name { get; set; }
        [Required] 
        public decimal Balance { get; set; }
    }
}