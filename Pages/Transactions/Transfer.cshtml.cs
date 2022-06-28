using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Models.Views;
using Microsoft.EntityFrameworkCore;
using BankingApp.Models.View;

namespace BankingApp.Pages.Transactions
{
    public class TransferModel : PageModel
    {
        private readonly Data.BankingAppContext _context;

        [BindProperty]
        public TransferVM TransferVM { get; set; }
        public decimal TransferAmount { get; set; }
        public List<string> ErrorMessages { get; set; }
        public SelectList FromAccounts { get; set; }
        public SelectList ToAccounts { get; set; }

        public TransferModel(Data.BankingAppContext context)
        {
            _context = context;
        }
        public async void OnGetAsync()
        {
            if(this.ErrorMessages == null) 
            {
                this.ErrorMessages = new List<string>();
            }
            this.FromAccounts = new SelectList(populateAccounts(), "Id", "Name");
            this.ToAccounts = new SelectList(populateAccounts(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (this.ErrorMessages == null)
                {
                    this.ErrorMessages = new List<string>();
                }

                var fromAccount = _context.Accounts.FindAsync(TransferVM.FromAccountId).Result;
                var toAccount = _context.Accounts.FindAsync(TransferVM.ToAccountId).Result;
                ValidateEntry(fromAccount, toAccount);
                // Fill out Tansfer View Model
                
                TransferVM.FromAccountBalance = fromAccount.Balance;

                if (ErrorMessages == null || ErrorMessages.Count == 0)
                {
                    
                    TransferVM.ToAccountBalance = toAccount.Balance;
                
                    TransferVM.TransactionTime = DateTime.Now;

                    // Update Account balances
                    fromAccount.Balance -= TransferVM.Amount;
                    toAccount.Balance += TransferVM.Amount; 
                
                    var entry = _context.Add(new Transaction());
                    entry.CurrentValues.SetValues(TransferVM);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch(DbUpdateException ex)
            {
                ErrorMessages.Add("There was an issue with this transfer");
                return Page();
            }
        }

        private List<Account> populateAccounts()
        {
            var Accounts = _context.Accounts.ToList();
            return Accounts;
        }

        private void ValidateEntry(Account fromAccount, Account toAccount)
        {
            if (fromAccount == null)
            {
                ErrorMessages.Add("Invalid From Account");
            }
            else if (fromAccount.Balance < TransferVM.Amount)
            {
                ErrorMessages.Add("Insufficient funds in account");
            }

            if (toAccount == null)
            {
                ErrorMessages.Add("Invalid To Account");
            }

            if(fromAccount != null && toAccount != null)
            {
                if(fromAccount.Id == toAccount.Id)
                {
                    ErrorMessages.Add("Same account selected for transfer. Please transfer between two seperate accounts");
                }
            }

            if(TransferVM != null && (TransferVM.Amount < 1 || TransferVM.Amount > 10000))
            {
                ErrorMessages.Add("Invalid Amount. Please enter an Amount between $1 and $10000");
            }
        }
    }
}
