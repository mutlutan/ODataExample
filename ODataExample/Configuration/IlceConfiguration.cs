using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ODataExample.Models;

namespace ODataExample.Configuration
{
    public class IlceConfiguration : IEntityTypeConfiguration<Ilce>
    {
        public void Configure(EntityTypeBuilder<Ilce> builder)
        {
            // Table name
            builder.ToTable("Ilceler");
            
            // Primary key
            builder.HasKey(i => i.Id);
            
            // Properties
            builder.Property(i => i.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
                
            builder.Property(i => i.Kod)
                .IsRequired()
                .HasMaxLength(10);
                
            builder.Property(i => i.Ad)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(i => i.SehirId)
                .IsRequired();
                
            // Relationships
            builder.HasOne(i => i.Sehir)
                .WithMany(s => s.Ilceler)
                .HasForeignKey(i => i.SehirId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Indexes
            builder.HasIndex(i => i.Kod)
                .HasDatabaseName("IX_Ilceler_Kod");
                
            builder.HasIndex(i => i.SehirId)
                .HasDatabaseName("IX_Ilceler_SehirId");
        }
    }
}
