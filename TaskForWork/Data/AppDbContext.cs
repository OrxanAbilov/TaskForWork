using Microsoft.EntityFrameworkCore;
using TaskForWork.Models;

namespace TaskForWork.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
