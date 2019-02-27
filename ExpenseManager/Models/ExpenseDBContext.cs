using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseManager.Models
{
    public class ExpenseDBContext : DbContext
    {
        public virtual DbSet<ExpenseReport> ExpenseReport { get; set; }
        IConfiguration configuration;

        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionstring = configuration.GetConnectionString("Expense");
                optionsBuilder.UseSqlServer(connectionstring);
            }
        }
    }
}