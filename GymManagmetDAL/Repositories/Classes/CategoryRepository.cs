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
    internal class CategoryRepository : GenericRepository<Category>
    {
        // ask clr injection object of type GymDBContext
        public CategoryRepository(GymDBContext dbContext) : base(dbContext)
        {
            
        }

        //public int Add(Category category)
        //{
        //    _dbcontext.Categories.Add(category);
        //    return _dbcontext.SaveChanges();
        //}

        //public int Delete(int Id)
        //{
        //    _dbcontext.Categories.Remove(_dbcontext.Categories.Find(Id)!);
        //    return _dbcontext.SaveChanges();
        //}

        //public IEnumerable<Category> GetAll() => _dbcontext.Categories.ToList();


        //public Category? GetById(int id) => _dbcontext.Categories.Find(id);


        //public int Update(Category category)
        //{
        //    _dbcontext.Categories.Update(category);
        //    return _dbcontext.SaveChanges();
        //}
    }
}
