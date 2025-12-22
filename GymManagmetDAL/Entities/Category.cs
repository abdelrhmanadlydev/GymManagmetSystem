using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public class Category :BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        // To use relation with Session
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
