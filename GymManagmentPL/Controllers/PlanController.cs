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
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        } 

        #endregion
    }
}
