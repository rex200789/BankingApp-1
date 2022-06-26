using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Models.Views;
using Microsoft.EntityFrameworkCore;

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
        public void OnGet()
        {
            this.FromAccounts = new SelectList(populateAccounts(), "Id", "Name");
            this.ToAccounts = new SelectList(populateAccounts(), "Id", "Name");
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        public SelectList FromAccounts { get; set; }
        public SelectList ToAccounts { get; set; }
        public decimal TransferAmount { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(TransferVM tm)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var entry = _context.Add(new Transaction());
                entry.CurrentValues.SetValues(Transaction);
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
