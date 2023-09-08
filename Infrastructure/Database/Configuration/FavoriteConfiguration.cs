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
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.Favorites)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Offer)
                .WithMany(p => p.FavoritesList)
                .HasForeignKey(p => p.OfferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
