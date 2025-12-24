using GymManagmetBLL.Service.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Interfasces
{
    internal interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int id);
        bool UpdatePlan(int id, UpdatePlanViewModel planToUpdate);
        bool ToggleStatus(int id);
    }
}
