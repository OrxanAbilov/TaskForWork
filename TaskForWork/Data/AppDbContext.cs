using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasOne(p => p.Item)
                .WithMany(b => b.Invoices);
        }
    }
}
