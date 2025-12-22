using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Membership : BaseEntity
    {
        // startdate == created at
        public DateTime EndDate { get; set; }
        public string Statues {
            get
            {
                if (this.EndDate > DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        #region Plane - Membership
        public int PlaneId { get; set; }
        public Plane Plane { get; set; } = null!;
        #endregion
    }
}
