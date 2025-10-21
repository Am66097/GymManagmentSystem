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

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
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
                session.AvailabaleSlots = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
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
            mappedSession.AvailabaleSlots = mappedSession.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(mappedSession.Id);
            return mappedSession;
            #endregion


        }

        
        public UpdateSessionViewModel? GetSessionForUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (session == null) return null;

            if (!IsSessionAvailableToUpdate(session)) return null; // هيرجع null لو مش upcoming أو فيه حجوزات

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        
        public bool UpdateSession(int sessionId, UpdateSessionViewModel UpdatedSession)
        {
            try
            {
                var session = _unitOfWork.sessionRepository.GetById(sessionId);
                if (session == null) return false;

                // السماح بالتحديث فقط إذا كانت الجلسة قادمة (Upcoming) ولا يوجد حجوزات
                if (!IsSessionAvailableToUpdate(session)) return false;

                if (!IsTrainerExists(UpdatedSession.TrainerId)) return false;
                if (!IsStartDateBeforeEndDate(UpdatedSession.StartDate, UpdatedSession.EndDate)) return false;

                _mapper.Map(UpdatedSession, session);
                session.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Update(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Failed Session : {ex}");
                return false;
            }
        }

        public bool DeleteSession(int sessionId)
        {

            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (session == null) return false;
                if (!IsSessionAvailableToRemove(session)) return false;
                _unitOfWork.GetRepository<Session>().Delete(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete Faild Session : {ex}");
                return false;
            }

        }

        public IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);

        }

        #region Helper Methods

        
        private bool IsSessionAvailableToRemove(Session session)
        {
            if (session == null) return false;

            var bookedMembersCount = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            if (bookedMembersCount > 0) return false;

            // نسمح بالحذف فقط إذا كانت قادمة (أو - حسب سياساتك - يمكنك السماح بحذف أي جلسة ليس لها حجوزات)
            return session.StartDate > DateTime.Now;
        }
        
        private bool IsSessionAvailableToUpdate(Session session)
        {
            if (session == null) return false;

            var now = DateTime.Now;

            // يجب أن تكون جلسة قادمة (StartDate > الآن)
            if (session.StartDate <= now) return false;

            // لا نسمح بتعديل الجلسات التي لها أعضاء حجزوا أماكن
            var bookedMembersCount = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            if (bookedMembersCount > 0) return false;

            return true;
        }

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
            return StartDate < EndDate && DateTime.Now < StartDate;
        }





        #endregion

    }
}
