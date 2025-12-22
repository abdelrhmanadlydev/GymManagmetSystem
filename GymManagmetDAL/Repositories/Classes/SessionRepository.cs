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
    internal class SessionRepository : GenericRepository<Session>
    {
        
        public SessionRepository(GymDBContext dbContext) : base(dbContext)
        {

        }
        //public int Add(Session session)
        //{
        //    _dbcontext.Sessions.Add(session);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.Sessions.Remove(_dbcontext.Sessions.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Session> GetAll() => _dbcontext.Sessions.ToList();


        //public Session? GetById(int id) => _dbcontext.Sessions.Find(id);


        //public int Update(Session session)
        //{
        //    _dbcontext.Sessions.Update(session);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
