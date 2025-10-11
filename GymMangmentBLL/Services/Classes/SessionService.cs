using AutoMapper;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {
            try
            {
                //Check if Trainer Exists
                if (!IsTrainerExists(CreatedSession.TrainerId)) return false;

                //Check if Category Exists
                if (!IsCategoryExists(CreatedSession.CategoryId)) return false;

                //Check if Start Date is before End Date
                if (!IsStartDateBeforeEndDate(CreatedSession.StartDate, CreatedSession.EndDate)) return false;

                var session = _mapper.Map<CreateSessionViewModel, Session>(CreatedSession);
                _unitOfWork.GetRepository<Session>().Add(session);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Create Faild Session : {ex}");
                return false;
            }



        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.sessionRepository.GetAllSessionsWithTrainersAndCategory();
            if (!Sessions.Any()) return [];

            #region Before Auto Mapper
            //return Sessions.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capacity,
            //    CategoryName = s.SessionCategory.CategoryName,//Related Data
            //    TrainerName = s.SessionTrainer.Name, //Related Data
            //    AvailabaleCapacity = s.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(s.Id) //Related Data

            //}); 
            #endregion

            #region After Auto Mapper
            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in mappedSessions)
                session.AvailabaleCapacity = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            return mappedSessions;


            #endregion


        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = _unitOfWork.sessionRepository.GetAllSessionsWithTrainersAndCategory(sessionId);
            if (session == null) return null;

            #region Before Auto Mapper
            //return new SessionViewModel
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    CategoryName = session.SessionCategory.CategoryName,//Related Data
            //    TrainerName = session.SessionTrainer.Name, //Related Data
            //    AvailabaleCapacity = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) //Related Data
            //}; 
            #endregion

            #region After Auto Mapper
            var mappedSession = _mapper.Map<Session, SessionViewModel>(session);
            mappedSession.AvailabaleCapacity = mappedSession.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(mappedSession.Id);
            return mappedSession;
            #endregion


        }

        #region Helper Methods

        private bool IsTrainerExists(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            return trainer != null;
        }

        private bool IsCategoryExists(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);
            return category != null;
        }
        private bool IsStartDateBeforeEndDate(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }

        #endregion

    }
}
