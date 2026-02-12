using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{ }  
        public DbSet<ShortLink> ShortLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortLink>()
                .HasIndex(s => s.ShortCode)
                .IsUnique();  // поиск + уникальность кодов
            base.OnModelCreating(modelBuilder);
        }
    }
}