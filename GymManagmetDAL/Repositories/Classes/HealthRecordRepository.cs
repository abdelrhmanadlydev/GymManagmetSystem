using GymManagmetDAL.Data.Context;
using GymManagmetDAL.Entities;
using GymManagmetDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Repositories.Classes
{
    internal class HealthRecordRepository : GenericRepository<HealthRecord>
    {

        // ask clr injection object of type GymDBContext
        public HealthRecordRepository(GymDBContext dbContext) : base(dbContext)
        {

        }
        //public int Add(HealthRecord healthRecord)
        //{
        //    _dbcontext.HealthRecords.Add(healthRecord);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.HealthRecords.Remove(_dbcontext.HealthRecords.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<HealthRecord> GetAll() => _dbcontext.HealthRecords.ToList();


        //public HealthRecord? GetById(int id) => _dbcontext.HealthRecords.Find(id);


        //public int Update(HealthRecord healthRecord)
        //{
        //    _dbcontext.HealthRecords.Update(healthRecord);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
