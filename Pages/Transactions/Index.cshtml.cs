using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data;
using BankingApp.Models;

namespace BankingApp.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly BankingAppContext _context;

        public IndexModel(BankingAppContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transactions != null)
            {
                Transaction = await _context.Transactions.Take(10).ToListAsync();
            }
        }
    }
}
