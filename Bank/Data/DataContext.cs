using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<AccountHistory> AccountHistories { get; set; }


    }
}
