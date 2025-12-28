using AutoMapper;
using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.ViewModels.SessionViewModel;
using GymManagmetDAL.Entities;
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
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionVeiwModel createSession)
        {
            try
            {
                // check if trainer is exists
                if (!IsTrainerExists(createSession.TrainerId))
                    return false;
                // check if category is exists
                if (!IsCategoryExists(createSession.CategoryId))
                    return false;
                // check if startDate is before endDate
                if (!IsDateTimeValid(createSession.StartDate, createSession.EndDate))
                    return false;
                if (createSession.Capacity < 0 || createSession.Capacity > 25)
                    return false;

                // Manual Mapping
                var sessionEntity = _mapper.Map<Session>(createSession);

                _unitOfWork.GetRepository<Session>().Add(sessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Faild {ex}");
                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategory();

            if (!sessions.Any()) return [];
            // Manual Mapping
            //return sessions.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capacty,
            //    TrainerName = s.SessionTrainer.Name,
            //    CategoryName = s.SessionCategory.CategoryName,
            //    AvailableSlot = s.Capacty - _unitOfWork.sessionRepository.GetCountOfBookedSlots(s.Id)
            //});

            // AutoMapper
            var MapedSession = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            // or var MapedSession = _mapper.Map<IEnumerable<Session>,<IEnumerable<SessionViewModel>>(sessions);
            foreach (var Sessions in MapedSession)
            {
                Sessions.AvailableSlot = Sessions.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(Sessions.Id);
            }
            return MapedSession;
        }
        public SessionViewModel? GetSessionById(int id)
        {
            var session = _unitOfWork.sessionRepository.GetSessionWithTrainerAndCategory(id);

            if (session == null) return null;

           var mappedSession = _mapper.Map<SessionViewModel>(session);
            // or var mappedSession = _mapper.Map<Session,SessionViewModel>(session);
            mappedSession.AvailableSlot = mappedSession.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(mappedSession.Id);
            return mappedSession;
        }
        public UpdateSessionVeiwModel? GetSessionForUpdate(int id)
        {
           var Session = _unitOfWork.GetRepository<Session>().GetById(id);
            if (!IsSessionAvailableToUpdate(Session!))
                return null;

            return _mapper.Map<UpdateSessionVeiwModel>(Session);
            // or return _mapper.Map<Session,UpdateSessionVeiwModel>(Session);
        }
        public bool UpdateSession(int id, UpdateSessionVeiwModel updateSession)
        {
            try
            {
                var Session = _unitOfWork.sessionRepository.GetById(id);
                if(!IsSessionAvailableToUpdate(Session!))
                    return false;
                if (!IsTrainerExists(updateSession.TrainerId))
                    return false;
                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate))
                    return false;

                // Manual Mapping
                _mapper.Map(updateSession, Session);
                Session!.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Session>().Update(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Faild {ex}");
                return false;
            }
        }
        public bool RemoveSession(int id)
        {
            try
            {
                var Session = _unitOfWork.GetRepository<Session>().GetById(id);
                if (!IsSessionAvailableToRemoving(Session!))
                    return false;

                _unitOfWork.GetRepository<Session>().Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Session Faild {ex}");
                return false;
            }
            
        }

        #region Helper Methods

        private bool IsTrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) != null;
        }
        private bool IsCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) != null;
        }
        private bool IsDateTimeValid(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }
        private bool IsSessionAvailableToUpdate(Session session)
        {
            if(session is null)
                return false;

            if (session.StartDate <= DateTime.Now)
                return false;

            if(session.EndDate <= DateTime.Now)
                return false;

            var HasActiveBookings = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (HasActiveBookings)
                return false;
                
            return true;
        }
        private bool IsSessionAvailableToRemoving(Session session)
        {
            if (session is null)
                return false;
            // if session is started
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now)
                return false;
            // if session is upcoming
            if (session.StartDate > DateTime.Now)
                return false;
            // if session has Active Booking
            var HasActiveBookings = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBookings)
                return false;

            return true;
        }

        #endregion
    }
}
