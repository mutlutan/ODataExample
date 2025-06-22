using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ODataExample.Models;

namespace ODataExample.Configuration
{
    public class UlkeConfiguration : IEntityTypeConfiguration<Ulke>
    {
        public void Configure(EntityTypeBuilder<Ulke> builder)
        {
            // Table name
            builder.ToTable("Ulkeler");
            
            // Primary key
            builder.HasKey(u => u.Id);
            
            // Properties
            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
                
            builder.Property(u => u.Kod)
                .IsRequired()
                .HasMaxLength(10);
                
            builder.Property(u => u.Ad)
                .IsRequired()
                .HasMaxLength(100);
                
            // Relationships
            builder.HasMany(u => u.Sehirler)
                .WithOne(s => s.Ulke)
                .HasForeignKey(s => s.UlkeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Indexes
            builder.HasIndex(u => u.Kod)
                .IsUnique()
                .HasDatabaseName("IX_Ulkeler_Kod");
        }
    }
}
