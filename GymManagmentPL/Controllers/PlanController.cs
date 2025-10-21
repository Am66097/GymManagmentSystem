using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            this._planService = planService;
        }

        #region Get All Plans
        public ActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        #endregion

        #region Details Of Plans 

        public ActionResult Details(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));

            }
            var plan = _planService.GetPlanById(id);

            if(plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(plan);
        }



        #endregion
    }
}
