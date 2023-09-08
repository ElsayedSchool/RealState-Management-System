using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Configuration
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.OwnerName).IsRequired().HasMaxLength(30);
            builder.HasIndex(p => p.OwnerName);

            builder.Property(p => p.Phone1).IsRequired().HasMaxLength(13);
            builder.HasIndex(p => p.Phone1);

            builder.Property(p => p.Phone2).HasMaxLength(13);
            builder.HasIndex(p => p.Phone2);

            builder.Property(p => p.Piece).IsRequired();
            builder.HasIndex(p => p.Piece);

            builder.Property(p => p.Kasema).IsRequired();
            builder.HasIndex(p => p.Kasema);

            builder.Property(p => p.Street);
            builder.HasIndex(p => p.Street);

            builder.Property(p => p.House).IsRequired();
            builder.HasIndex(p => p.House);

            builder.Property(p => p.Price).IsRequired();
            builder.HasIndex(p => p.Price);

            builder.Property(p => p.Details);
            builder.HasIndex(p => p.Details);

            builder.Property(p => p.AreaId).IsRequired(false);

            builder.Property(p => p.LocationId).IsRequired(false);

            builder.Property(p => p.SectionId).IsRequired(false);
            
            builder.Property(p => p.DistributionId).IsRequired(false);

            builder.Property(p => p.CreatedById).IsRequired();

            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.ModifiedById).IsRequired(false);
            
            builder.Property(p => p.ModifiedAt).IsRequired(false);


            builder
                .HasOne(p => p.Department)
                .WithMany(p => p.Departments)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Purpose)
                .WithMany(p => p.Purposes)
                .HasForeignKey(p => p.PurposeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Type)
                .WithMany(p => p.Types)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Area)
                .WithMany(p => p.Areas)
                .HasForeignKey(p => p.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Location)
                .WithMany(p => p.Locations)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Section)
                .WithMany(p => p.Sections)
                .HasForeignKey(p => p.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Distribution)
                .WithMany(p => p.Distributions)
                .HasForeignKey(p => p.DistributionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.CreatedBy)
                .WithMany(p => p.CreatedOffers)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.ModifiedBy)
                .WithMany(p => p.ModifiedOffers)
                .HasForeignKey(p => p.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
