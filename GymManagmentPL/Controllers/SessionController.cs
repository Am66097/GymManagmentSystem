using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id !";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionById(id);

            if(session is  null)
            {
                TempData["ErrorMessage"] = "Session Not Found !";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #endregion
    }
}
