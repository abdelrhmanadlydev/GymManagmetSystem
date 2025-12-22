using GymManagmetDAL.Data.Context;
using GymManagmetDAL.Entities;
using GymManagmetDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        //private readonly GymDBContext _dbcontext = new GymDBContext();

        // Using Dependency Injection
        private readonly GymDBContext _dbcontext;

        // ask clr injection object of type GymDBContext to get database connection string
        public GenericRepository(GymDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int Add(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
            return _dbcontext.SaveChanges();
        }
 
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
                return _dbcontext.Set<TEntity>().AsNoTracking().ToList();

            else
                return _dbcontext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        public TEntity? GetById(int id) => _dbcontext.Set<TEntity>().Find(id);

        public int Update(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
            return _dbcontext.SaveChanges();
        }
    }
}
