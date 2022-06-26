using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data;
using BankingApp.Models;

namespace BankingApp.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly BankingAppContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(BankingAppContext context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Account Account { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Account == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete",
                                        new { id, saveChangesError = true });
            }
        }
    }
}
