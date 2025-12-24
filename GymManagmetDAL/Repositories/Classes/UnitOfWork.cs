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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();

        private readonly GymDBContext _dbContext;
        public UnitOfWork(GymDBContext dbContext, ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            this.sessionRepository = sessionRepository;
        }

        public ISessionRepository sessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            // [key] ==> tyep of (TEntity)
            var entityType = typeof(TEntity);

            if(_repositories.TryGetValue(entityType, out var repo))
            {
                return (IGenericRepository<TEntity>)repo;
            }

            // object ==> new GenericRepository<TEntity>(Context)
            var newRepository = new GenericRepository<TEntity>(_dbContext);
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
