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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb => 
            {
                tb.HasCheckConstraint("SessionCapactyCheck", "Capacty Between 1 and 25");
                tb.HasCheckConstraint("SessionEndDate", "EndDate > StartDate");
            });

            // Relation (one to Many) Betwen Sessions & Category
            builder.HasOne(x=>x.SessionCategory).WithMany(x=>x.Sessions).HasForeignKey(x=>x.CategoryId);

            // Relation (one to Many) Betwen Sessions & Trainer
            builder.HasOne(x=>x.SessionTrainer).WithMany(x=>x.TrainerSession).HasForeignKey(x=>x.TrainerId);
        }
    }
}
