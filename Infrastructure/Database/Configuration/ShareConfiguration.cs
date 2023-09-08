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
    public class ShareConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.TransferFromUser)
                .WithMany(p => p.SharedFromList)
                .HasForeignKey(p => p.SharedFromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.TransferToUser)
                .WithMany(p => p.SharedToList)
                .HasForeignKey(p => p.SharedToId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(p => p.Offer)
                .WithMany(p => p.SharedList)
                .HasForeignKey(p => p.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
