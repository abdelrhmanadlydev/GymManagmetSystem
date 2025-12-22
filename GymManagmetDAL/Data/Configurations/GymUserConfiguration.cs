using GymManagmetDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X => X.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(X => X.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(X => X.Phone).HasColumnType("varchar").HasMaxLength(11);

            builder.ToTable(tb =>
            {
                //                          Constraint Name
                tb.HasCheckConstraint("GymUserValidEmailCheck","Email LIKE '_%@_%._%' AND Email NOT LIKE '% %'");

                tb.HasCheckConstraint("GymUserValidPhoneCheck","Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");

            });

            builder.HasIndex(x=>x.Email).IsUnique();
            builder.HasIndex(x=>x.Phone).IsUnique();

            builder.OwnsOne(x => x.Address, addressbuilder => 
            {
                addressbuilder.Property(x => x.Street).HasColumnName("street").HasColumnType("varchar").HasMaxLength(30);
                addressbuilder.Property(x => x.City).HasColumnName("city").HasColumnType("varchar").HasMaxLength(30);
                addressbuilder.Property(x => x.BuildingNumber).HasColumnName("BuildingNumber");
            });
        }
    }
}
