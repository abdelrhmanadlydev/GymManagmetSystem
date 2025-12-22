using GymManagmetDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Data.Context
{
    public class GymDBContext : DbContext
    {
        // to Add New Migration Right (
        // Add-Migration "InitialCreate" -OutputDir "Data/Migrations" -project "GymManagmetDAL" -StartupProject "GymManagmetPL") in Package Manger Console.

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database= GymManagmentG02;Trusted_Connection=true;TrustServerCertificate= true");
        //}

        public GymDBContext(DbContextOptions<GymDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DBSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        #endregion
    }
}
