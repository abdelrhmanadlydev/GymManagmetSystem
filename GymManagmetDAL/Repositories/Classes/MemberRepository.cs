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
    internal class MemberRepository : GenericRepository<Member>
    {
       
        // ask clr injection object of type GymDBContext
        public MemberRepository(GymDBContext dbContext) : base(dbContext)
        {

        }

        //public int Add(Member member)
        //{
        //    _dbcontext.Members.Add(member);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.Members.Remove(_dbcontext.Members.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Member> GetAll() => _dbcontext.Members.ToList();


        //public Member? GetById(int id) => _dbcontext.Members.Find(id);


        //public int Update(Member member)
        //{
        //    _dbcontext.Members.Update(member);
        //    return _dbcontext.SaveChanges();
        //}
    }   
}
