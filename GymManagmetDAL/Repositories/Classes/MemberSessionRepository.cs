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
    internal class MemberSessionRepository : GenericRepository<MemberSession>
    {

        // ask clr injection object of type GymDBContext        //chain of base responsibility
        public MemberSessionRepository(GymDBContext dbContext) : base(dbContext)
        {

        }
        //public int Add(MemberSession memberSession)
        //{
        //    _dbcontext.MemberSessions.Add(memberSession);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.MemberSessions.Remove(_dbcontext.MemberSessions.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<MemberSession> GetAll() => _dbcontext.MemberSessions.ToList();


        //public MemberSession? GetById(int id) => _dbcontext.MemberSessions.Find(id);


        //public int Update(MemberSession memberSession)
        //{
        //    _dbcontext.MemberSessions.Update(memberSession);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
