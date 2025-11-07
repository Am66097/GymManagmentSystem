
using AutoMapper;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.ViewModels.SessionScheduls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentPL.Controllers
{
    public class SessionsScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMemberSessionRepository _memberSessionRepository;
        private readonly IMemberRepository _memberRepository;

        public SessionsScheduleController(IUnitOfWork unitOfWork, IMapper mapper, ISessionRepository sessionRepository, IMemberSessionRepository memberSessionRepository, IMemberRepository memberRepository)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            this._sessionRepository = sessionRepository;
            this._memberSessionRepository = memberSessionRepository;
            this._memberRepository = memberRepository;
        }
        #region Index
        public ActionResult Index()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithDetails();

            var viewModel = _mapper.Map<List<MemberSessionViewModel>>(sessions);

            return View(viewModel);
        }
        #endregion


        #region CreateBooking (GET)
        [HttpGet]
        public ActionResult CreateBooking(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if (session == null)
                return NotFound();

            var members = _unitOfWork.MemberRepository
                .GetAll()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                })
                .ToList();

            var model = new CreateBookingViewModel
            {
                SessionId = session.Id,
                Date = session.CreatedAt,
                Members = members
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateBooking(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Members = _unitOfWork.MemberRepository
                    .GetAll()
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Name
                    })
                    .ToList();

                return View(model);
            }

            var memberSession = _mapper.Map<MemberSession>(model);
            _unitOfWork.MemberSessionRepository.Add(memberSession);
            _unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Booking created successfully!";
            return RedirectToAction("GetMembersForUpcomingSession", new { sessionId = model.SessionId });
        }
        #endregion


        #region GetMembersForUpcomingSession
        [HttpGet]
        public ActionResult GetMembersForUpcomingSession(int sessionId)
        {
            var session = _unitOfWork.SessionRepository
                .GetAllSessionsWithDetails()
                .FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                return NotFound();

            var bookings = _unitOfWork.MemberSessionRepository.GetBySessionId(sessionId);

            var viewModel = _mapper.Map<SessionMembersViewModel>(session);
            viewModel.Members = _mapper.Map<List<MemberBookingInfo>>(bookings);
            viewModel.SessionId = sessionId;

            return View(viewModel);
        }


        #endregion

        #region Cancel
        [HttpPost]
        public ActionResult CancelBooking(int memberSessionId, int sessionId)
        {
            var booking = _unitOfWork.MemberSessionRepository
                .GetAll()
                .FirstOrDefault(ms => ms.Id == memberSessionId);

            if (booking == null)
            {
                TempData["ErrorMessage"] = "Booking not found!";
                return RedirectToAction(nameof(GetMembersForUpcomingSession), new { sessionId });
            }

            _unitOfWork.MemberSessionRepository.Delete(booking);
            _unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Booking cancelled successfully!";
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { sessionId });
        }
        #endregion

        #region GetMembersForOngoingSession

        [HttpGet]
        public ActionResult GetMembersForOngoingSessions(int sessionId)
        {
            var session = _unitOfWork.SessionRepository
                .GetAllSessionsWithDetails()
                .FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                return NotFound();

            var memberSessions = _unitOfWork.MemberSessionRepository.GetBySessionId(sessionId);

            var viewModel = _mapper.Map<SessionMembersViewModel>(session);
            viewModel.Members = _mapper.Map<List<MemberBookingInfo>>(memberSessions);
            viewModel.SessionId = sessionId;

            return View(viewModel);
        }

        #endregion


        #region MarkAttendance

        [HttpPost]
        public ActionResult MarkAttendance(int memberSessionId, int sessionId)
        {
            var booking = _unitOfWork.MemberSessionRepository
                .GetAll()
                .FirstOrDefault(ms => ms.Id == memberSessionId);

            if (booking == null)
            {
                TempData["ErrorMessage"] = "Booking not found!";
                return RedirectToAction(nameof(GetMembersForOngoingSessions), new { sessionId });
            }

            booking.IsAttended = true;
            booking.AttendanceDate = DateTime.Now;

            _unitOfWork.MemberSessionRepository.Update(booking);
            _unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Member marked as attended!";
            return RedirectToAction(nameof(GetMembersForOngoingSessions), new { sessionId });
        }

        #endregion


    }
}



