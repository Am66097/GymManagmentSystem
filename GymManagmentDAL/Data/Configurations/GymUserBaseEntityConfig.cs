using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GymManagmentDAL.Entities;

namespace GymManagmentDAL.Data.Configurations
{
    internal class GymUserBaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
               .HasColumnType("varchar")
               .HasMaxLength(100);

            builder.Property(x=>x.PhoneNumber)
                .HasColumnType("varcahr")
                .HasMaxLength(11);

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("GymUserVaildEmailCheck", "Email like '_%@_%._%'");
                Tb.HasCheckConstraint("GymUserVaildPhoneCheck", "Phone Like '01%' and Phone Not like '%[^0-9]%'");
            });

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            builder.OwnsOne(x => x.Address, AddreddBuilder => 
            {
            
                AddreddBuilder.Property(x=>x.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddreddBuilder.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddreddBuilder.Property(x => x.BuildingNumber)
                .HasColumnName("BuilderNumber");
            });

        }
    }
}
