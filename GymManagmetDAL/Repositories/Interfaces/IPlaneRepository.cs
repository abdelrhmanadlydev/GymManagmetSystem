using GymManagmetDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Repositories.Interfaces
{
    public interface IPlaneRepository
    {
        IEnumerable<Plane> GetAll();

        Plane? GetById(int id);

        int Update(Plane plane);


    }
}
