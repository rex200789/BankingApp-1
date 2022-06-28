using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankingApp.Models;

namespace BankingApp.Data
{
    public class BankingAppContext : DbContext
    {
        public BankingAppContext (DbContextOptions<BankingAppContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
                                              
                                              


        }
    }
}
