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
    internal class TrainerRepository : GenericRepository<Trainer>
    {
        // ask clr injection object of type GymDBContext
        public TrainerRepository(GymDBContext dbContext) : base(dbContext)
        {

        }
       
        //public int Add(Trainer trainer)
        //{
        //    _dbcontext.Trainers.Add(trainer);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.Trainers.Remove(_dbcontext.Trainers.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Trainer> GetAll() => _dbcontext.Trainers.ToList();


        //public Trainer? GetById(int id) => _dbcontext.Trainers.Find(id);


        //public int Update(Trainer trainer)
        //{
        //    _dbcontext.Trainers.Update(trainer);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
