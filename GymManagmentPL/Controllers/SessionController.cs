using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    [Authorize]
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
            var model = new CreateSessionViewModel
            {
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(2)
            };
            DropDowns();
            return View(model);
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

        #region Edit Sessions

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var Session = _sessionService.GetSessionForUpdate(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            DropDownsForTrainers();
            return View(Session);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute]int id, UpdateSessionViewModel UpdatedSession)
        {
            if (!ModelState.IsValid)
            {
                DropDownsForTrainers();
                return View(UpdatedSession);
            }
             
            var result =_sessionService.UpdateSession(id, UpdatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Session !";

            }
            return RedirectToAction(nameof(Index));
        }


        #endregion

        #region Delete Session 
        // GET: Session/Delete/4
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = id;
            return View(session);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.DeleteSession(id);
            if (result)
                TempData["SuccessMessage"] = "Session deleted successfully.";
            else
                TempData["ErrorMessage"] = "Cannot delete session: either it's upcoming with bookings or ongoing.";

            return RedirectToAction(nameof(Index));
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
        private void DropDownsForCategories()
        {
            var Categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");

         

        }
        private void DropDownsForTrainers()
        {
         

            var Trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");

        }



        #endregion

    }
}
