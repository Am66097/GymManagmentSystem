using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.PlanViewModels;
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

        #region Edit Plans

        public ActionResult Edit(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id !";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanToUpdate(id);

            if(plan is null)
            {
                TempData["ErrorMessage"] = "Plan Can Not updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute]int id ,UpdatePlanViewModel UpdatedPlan)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(UpdatedPlan);
            }
            var result = _planService.UpdatePlan(id,UpdatedPlan);
            if(result)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To update";

            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
