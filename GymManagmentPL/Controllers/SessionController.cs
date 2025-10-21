using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Get All Sessions
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        #endregion

        #region Session Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id !";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionById(id);

            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found !";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #endregion

        #region Create Sessions

        public ActionResult Create()
        {
            DropDowns();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel CreatedSession)
        {
            if (!ModelState.IsValid)
            {
                DropDowns();
                return View(CreatedSession);
            }


            var result = _sessionService.CreateSession(CreatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["ErrorMessage"] = "Failed To Create Session !";
                    DropDowns();
                return View(CreatedSession);
            }
        }

        #endregion

        #region Helper Method

        private void DropDowns()
        {
            var Categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");

            var Trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");

        }
        #endregion

    }
}
