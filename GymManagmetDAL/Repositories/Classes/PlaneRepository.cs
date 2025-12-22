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
    public class PlaneRepository : IPlaneRepository
    {
        //private readonly GymDBContext _dbcontext = new GymDBContext();

        // Using Dependency Injection
        private readonly GymDBContext _dbcontext;

        // ask clr injection object of type GymDBContext
        public PlaneRepository(GymDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
       
        public IEnumerable<Plane> GetAll() => _dbcontext.Planes.ToList();


        public Plane? GetById(int id) => _dbcontext.Planes.Find(id);


        public int Update(Plane plane)
        {
            _dbcontext.Planes.Update(plane);
            return _dbcontext.SaveChanges();
        }
    }
}
