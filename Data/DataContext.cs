using Microsoft.EntityFrameworkCore;
using WineReviewsApplication.Models;

namespace WineReviewsApplication.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            
        }
        public DbSet<WineType> WineTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Vineyard> Vineyards { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<WineVineyard> WineVineyards { get; set; }
        public DbSet<WineCategory> WineCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WineCategory>()
                .HasKey(pc => new { pc.WineId, pc.CategoryId });
            modelBuilder.Entity<WineCategory>()
                .HasOne(p => p.Wine)
                .WithMany(pc => pc.WineCategories)
                .HasForeignKey(p => p.WineId);
            modelBuilder.Entity<WineCategory>()
                .HasOne(p => p.WineType)
                .WithMany(pc => pc.WineCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<WineVineyard>()
                .HasKey(po => new { po.WineId, po.VineyardId });
            modelBuilder.Entity<WineVineyard>()
                .HasOne(p => p.Wine)
                .WithMany(pc => pc.WineVineyards)
                .HasForeignKey(p => p.WineId);
            modelBuilder.Entity<WineVineyard>()
                .HasOne(p => p.Vineyard)
                .WithMany(pc => pc.WineVineyards)
                .HasForeignKey(c => c.VineyardId);


        }
    }
}
