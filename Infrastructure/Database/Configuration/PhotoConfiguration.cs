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
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PhotoUrl).IsRequired().HasMaxLength(250);

            builder
                .HasOne(p => p.Offer)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.OfferId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(p => p.Offer);

        }
    }
}
