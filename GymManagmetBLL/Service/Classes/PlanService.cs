using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.Service.ViewModels.PlanViewModel;
using GymManagmetDAL.Entities;
using GymManagmetDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Classes
{
    internal class PlanService : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plane>().GetAll();

            if (!plans.Any()) return [];

           return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            });
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepository<Plane>().GetById(id);

            if (plan == null) return null;

            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepository<Plane>().GetById(id);

            if (plan == null || HasActiveMemberships(id)) return null;

            return new UpdatePlanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.price
            };
        }
        public bool UpdatePlan(int id, UpdatePlanViewModel planToUpdate)
        {
            var plan = _unitOfWork.GetRepository<Plane>().GetById(id);

            if (plan == null || HasActiveMemberships(id)) return false;

            (plan.Name, plan.Description, plan.DurationDays, plan.price) =
                (planToUpdate.PlanName, planToUpdate.Description,
                 planToUpdate.DurationDays, planToUpdate.Price);
            _unitOfWork.GetRepository<Plane>().Update(plan);
            return _unitOfWork.SaveChanges() > 0;

        }

        public bool ToggleStatus(int id)
        {
            var repo = _unitOfWork.GetRepository<Plane>();
            var plan = repo.GetById(id);
            if (plan == null || HasActiveMemberships(id)) return false;

            plan.IsActive = plan.IsActive == true ? false : true;
            plan.UpdatedAt = DateTime.UtcNow;
            try
            {
                repo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
      
        #region Helper Methods
        private bool HasActiveMemberships(int planId)
        {
            var memberships = _unitOfWork.GetRepository<Membership>().GetAll(x=>x.PlaneId == planId && x.Statues == "Active");
            return memberships.Any();
        }
        #endregion
    }
}
