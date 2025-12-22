using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region Session - Category
        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; } = null!;

        #endregion

        #region Session - Trainer
        public int TrainerId { get; set; }
        public Trainer SessionTrainer { get; set; } = null!;
        #endregion

        #region Session - MemberSession
        ICollection<MemberSession> SessionMembers { get; set; } 
        #endregion
    }
}
