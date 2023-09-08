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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Detail).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Date).IsRequired();
            
            builder
                .HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Offer)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

    /*        builder.Ignore(p => p.Offer);
            builder.Ignore(p => p.User);*/
        }
    }
}
