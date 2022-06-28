using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data;
using BankingApp.Models;

namespace BankingApp.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly BankingAppContext _context;

        public EditModel(BankingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            Account =  await _context.Accounts.FindAsync(id);
            
            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var accountToUpdate = await _context.Accounts.FindAsync(id);

            if(accountToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Account>(
                accountToUpdate,
                "account",
                a => a.Id,
                a => a.Name,
                a => a.Balance))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
