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
    internal class MembershipRepository : GenericRepository<Membership>
    {
       
        // ask clr injection object of type GymDBContext
        public MembershipRepository(GymDBContext dbContext) : base(dbContext)
        {

        }

        //public int Add(Membership membership)
        //{
        //    _dbcontext.Memberships.Add(membership);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.Memberships.Remove(_dbcontext.Memberships.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Membership> GetAll() => _dbcontext.Memberships.ToList();


        //public Membership? GetById(int id) => _dbcontext.Memberships.Find(id);


        //public int Update(Membership membershipmber)
        //{
        //    _dbcontext.Memberships.Update(membershipmber);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
