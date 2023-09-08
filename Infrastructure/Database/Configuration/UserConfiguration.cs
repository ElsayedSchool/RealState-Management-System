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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasMaxLength(60);

            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            
            builder.Property(p => p.Phone).HasMaxLength(13).IsRequired(false);

            builder
                .HasOne(p => p.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
