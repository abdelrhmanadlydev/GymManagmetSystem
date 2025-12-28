using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.ViewModels.AnalyticsViewModel;
using GymManagmetDAL.Entities;
using GymManagmetDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Classes
{
    public class AnalutiysService : IAnalutiysService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalutiysService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnalutiysViewModels GetAnalutiysData()
        {
            var sessions = _unitOfWork.GetRepository<Session>().GetAll();
            return new AnalutiysViewModels
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(m => m.Statues == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainer = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = sessions.Where(x => x.StartDate > DateTime.Now).Count(),
                OngoingSession = sessions.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).Count(),
                CompleteSessions = sessions.Where(x => x.EndDate < DateTime.Now).Count(),
            };
        }
    }
}
