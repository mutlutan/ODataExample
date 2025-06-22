using Microsoft.EntityFrameworkCore;
using ODataExample.Models;
using ODataExample.Configuration;

namespace ODataExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity Configurations
            modelBuilder.ApplyConfiguration(new UlkeConfiguration());
            modelBuilder.ApplyConfiguration(new SehirConfiguration());
            modelBuilder.ApplyConfiguration(new IlceConfiguration());

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Ülke verileri
            modelBuilder.Entity<Ulke>().HasData(
                new Ulke { Id = 1, Kod = "TR", Ad = "Türkiye" },
                new Ulke { Id = 2, Kod = "US", Ad = "Amerika Birleşik Devletleri" },
                new Ulke { Id = 3, Kod = "DE", Ad = "Almanya" }
            );

            // Şehir verileri
            modelBuilder.Entity<Sehir>().HasData(
                new Sehir { Id = 1, Kod = "34", Ad = "İstanbul", UlkeId = 1 },
                new Sehir { Id = 2, Kod = "06", Ad = "Ankara", UlkeId = 1 },
                new Sehir { Id = 3, Kod = "35", Ad = "İzmir", UlkeId = 1 },
                new Sehir { Id = 4, Kod = "NY", Ad = "New York", UlkeId = 2 },
                new Sehir { Id = 5, Kod = "CA", Ad = "California", UlkeId = 2 },
                new Sehir { Id = 6, Kod = "BE", Ad = "Berlin", UlkeId = 3 }
            );

            // İlçe verileri
            modelBuilder.Entity<Ilce>().HasData(
                new Ilce { Id = 1, Kod = "BES", Ad = "Beşiktaş", SehirId = 1 },
                new Ilce { Id = 2, Kod = "FTH", Ad = "Fatih", SehirId = 1 },
                new Ilce { Id = 3, Kod = "KDK", Ad = "Kadıköy", SehirId = 1 },
                new Ilce { Id = 4, Kod = "CHK", Ad = "Çankaya", SehirId = 2 },
                new Ilce { Id = 5, Kod = "KZL", Ad = "Keçiören", SehirId = 2 },
                new Ilce { Id = 6, Kod = "KRS", Ad = "Karşıyaka", SehirId = 3 },
                new Ilce { Id = 7, Kod = "BRN", Ad = "Bornova", SehirId = 3 }
            );
        }
    }
}
