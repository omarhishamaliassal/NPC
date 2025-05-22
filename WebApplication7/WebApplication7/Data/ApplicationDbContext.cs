using Microsoft.EntityFrameworkCore;
using WebApplication7.Models; // ✅ Ensure this matches the actual namespace of your models

namespace WebApplication7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ DbSets for your tables
        public DbSet<NewsAPI> News { get; set; }
       public DbSet<AboutAPI> About { get; set; }
        public DbSet<LkpUser> LkpUsers { get; set; }
        //public DbSet<About> About { get; set; }
        //public DbSet<News> News { get; set; }



        // ✅ DbSet for VGovStat view (read-only)
        public DbSet<VGovStatAPI> VGovStat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure VGovStat as a view with no key (read-only)
            modelBuilder.Entity<VGovStatAPI>().HasNoKey().ToView("VGovStat");
        }
    }
}
