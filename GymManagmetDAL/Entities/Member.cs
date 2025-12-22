using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Member : GymUser
    {
        // JoinDate == Createdat of BaseEntity
        public string? Photo { get; set; }

        #region Member - HealthRecord
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - Membership
        public ICollection<Membership> Memberships { get; set; } = null!;
        #endregion

        #region Member - MemberSession 
        ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion
    }
}
