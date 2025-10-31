
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
            var sessions = _sessionRepository.GetAllSessionsWithDetails();

            var viewModel = _mapper.Map<List<MemberSessionViewModel>>(sessions);

            return View(viewModel);
        }

        #endregion

        #region GetMembersForUpcomingSession Action 

        [HttpGet]
        public IActionResult GetMembersForUpcomingSession(int sessionId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelBooking(int memberSessionId, int sessionId)
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


        #region MemberSession(SessionSchedule) Create Action

        //[HttpGet]
        //public IActionResult CreateBooking(int sessionId)
        //{
        //    var model = new CreateBookingViewModel
        //    {
        //        SessionId = sessionId
        //    };

        //    // جلب كل الأعضاء لعرضهم في Select
        //    ViewBag.Members = new SelectList(_memberRepository.GetAll(), "Id", "FullName");

        //    return View(model);
        //}
        [HttpGet]
        public IActionResult CreateBooking(int sessionId)
        {
            var members = _unitOfWork.MemberRepository
                            .GetAll()
                            .Where(m => !string.IsNullOrEmpty(m.Name))
                            .Select(m => new SelectListItem
                            {
                                Value = m.Id.ToString(),
                                Text = m.Name
                            })
                            .ToList();

            var model = new CreateBookingViewModel
            {
                SessionId = sessionId,
                Members = members
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // لو في خطأ نرجع نفس القائمة للـ dropdown
                model.Members = _unitOfWork.MemberRepository
                                    .GetAll()
                                    .Where(m => !string.IsNullOrEmpty(m.Name))
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


        #region GetMembersForOngoingSession Action 

        #endregion
    }
}



