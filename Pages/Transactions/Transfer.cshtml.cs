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

        //private readonly ILogger<TransferModel> _logger;
        public TransferModel(Data.BankingAppContext context
                            )
        {
            _context = context;
        }
        public async void OnGetAsync()
        {
            this.FromAccounts = new SelectList(populateAccounts(), "Id", "Name");
            this.ToAccounts = new SelectList(populateAccounts(), "Id", "Name");
        }

        
        public Transaction Transaction { get; set; }
        public IList<TransactionVM> Transactions { get; set; }

        public SelectList FromAccounts { get; set; }
        public SelectList ToAccounts { get; set; }
        [BindProperty]
        public TransferVM TransferVM { get; set; }
        public decimal TransferAmount { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Fill out Tansfer View Model
                var fromAccount = _context.Accounts.FindAsync(TransferVM.FromAccountId).Result;
                TransferVM.FromAccountBalance = fromAccount.Balance;

                var toAccount = _context.Accounts.FindAsync(TransferVM.ToAccountId).Result;
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
            catch(DbUpdateException ex)
            {
                return RedirectToPage("./Index");
                // _logger.LogError(ex, ErrorMessage);
                //return RedirectToAction("./Transfer",
                //                 new { tm.Id, tm.FromAccountId, tm.ToAccountId, tm.Amount, saveChangesError = true });
            }
        }

        private List<Account> populateAccounts()
        {
            var Accounts = _context.Accounts.ToList();
            return Accounts;
        }
    }
}
