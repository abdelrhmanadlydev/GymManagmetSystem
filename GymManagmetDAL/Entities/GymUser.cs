using GymManagmetDAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetDAL.Entities
{
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }

    //for use (Owned) from EFCore _ instal (Microsoft.EntityFrameworkCore.SqlServer) (V9.0.11)
    [Owned] 
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
