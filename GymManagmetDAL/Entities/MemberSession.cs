using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class MemberSession : BaseEntity
    {

        // bookingDate == created at 
        public bool IsAttended { get; set; }
        public int MemberId { get; set; }
        Member Member { get; set; } = null!;

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}
