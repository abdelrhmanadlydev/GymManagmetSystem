using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.ViewModels.PlanViewModel
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal DurationDays { get; set; }
        public bool IsActive { get; set; }


    }
}
