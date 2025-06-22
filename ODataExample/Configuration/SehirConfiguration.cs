using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ODataExample.Models;

namespace ODataExample.Configuration
{
    public class SehirConfiguration : IEntityTypeConfiguration<Sehir>
    {
        public void Configure(EntityTypeBuilder<Sehir> builder)
        {
            // Table name
            builder.ToTable("Sehirler");
            
            // Primary key
            builder.HasKey(s => s.Id);
            
            // Properties
            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
                
            builder.Property(s => s.Kod)
                .IsRequired()
                .HasMaxLength(10);
                
            builder.Property(s => s.Ad)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(s => s.UlkeId)
                .IsRequired();
                
            // Computed property - veritabanında saklanmaz
            builder.Ignore(s => s.Nufusu);
                
            // Relationships
            builder.HasOne(s => s.Ulke)
                .WithMany(u => u.Sehirler)
                .HasForeignKey(s => s.UlkeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(s => s.Ilceler)
                .WithOne(i => i.Sehir)
                .HasForeignKey(i => i.SehirId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Indexes
            builder.HasIndex(s => s.Kod)
                .HasDatabaseName("IX_Sehirler_Kod");
                
            builder.HasIndex(s => s.UlkeId)
                .HasDatabaseName("IX_Sehirler_UlkeId");
        }
    }
}
