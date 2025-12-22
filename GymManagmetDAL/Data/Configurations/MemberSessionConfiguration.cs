using GymManagmetDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Data.Configurations
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Property(x => x.CreatedAt).HasColumnName("BookingDate").HasDefaultValueSql("GETDATE()");

            // to use Composite Primary Key
            builder.HasKey(x => new { x.MemberId, x.SessionId });

            // Ignore Id from BaseEntity Becouse it is Not Useful
            builder.Ignore(x => x.Id);
        }
    }
}
