using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.Service.ViewModels.SessionViewModel;
using GymManagmetDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Classes
{
    public class SessionService : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategory();

            if (!sessions.Any()) return [];
            return sessions.Select(s => new SessionViewModel
            {
                Id = s.Id,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacty,
                TrainerName = s.SessionTrainer.Name,
                CategoryName = s.SessionCategory.CategoryName,
                AvailableSlot = s.Capacty - _unitOfWork.sessionRepository.GetCountOfBookedSlots(s.Id)
            });
        }
       
    }
}
