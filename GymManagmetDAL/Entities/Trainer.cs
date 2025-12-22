using GymManagmetDAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Trainer : GymUser
    {
        // hiredate == createdat of BaseEntity
        public Specialies Specialies { get; set; }

        #region Trainer - session

        public ICollection<Session> TrainerSession { get; set; } = null!;

        #endregion
    }
}
