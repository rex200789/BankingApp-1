using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankingApp.Models;
using BankingApp.Models.Views;

namespace BankingApp.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly Data.BankingAppContext _context;

        public CreateModel(Data.BankingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountVM AccountVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new AccountVM());
            entry.CurrentValues.SetValues(AccountVM);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        /*
        [BindProperty]
        public Account Account { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var newAccount = new Account();
           
            if (await TryUpdateModelAsync<Account>(newAccount,
                "account", 
                a => a.Name, a => a.Balance))
            {
                _context.Accounts.Add(newAccount);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
        */


    }
}
