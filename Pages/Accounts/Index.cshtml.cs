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
    public class IndexModel : PageModel
    {
        private readonly BankingApp.Data.BankingAppContext _context;

        public IndexModel(BankingApp.Data.BankingAppContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Account = await _context.Accounts.Take(10).ToListAsync();
            }
        }
    }
}
