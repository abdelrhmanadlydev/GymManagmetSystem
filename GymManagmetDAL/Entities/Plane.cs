using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Plane : BaseEntity
    {
        public String Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal price { get; set; }
        public bool IsActive { get; set; }

        #region plane - Membership
        public ICollection<Membership> PlanMembership { get; set; }
        #endregion

    }
}
