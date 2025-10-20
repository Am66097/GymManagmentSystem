using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemberViewModels;
using GymMangmentBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this._trainerService = trainerService;
        }

        #region Get All Trainer

        public IActionResult Index()
        {

            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        #endregion

        #region Create Trainer 

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel model)
        {



            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Fields ");
                return View(nameof(Create), model);
            }

            var result = _trainerService.CreateTrainer(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully . ";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Create , Phone Number Or Email Already Exists . ";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Details Of Trainers

        public IActionResult Details(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id !";
                return RedirectToAction(nameof(Index));
            }
             var trainer = _trainerService.GetTrainerDetailsById(id);

            if(trainer == null)
            {
                TempData["ErrorMessage"] = " Trainer Not Found !";
                return RedirectToAction(nameof(Index));
            }
       return View(trainer);
        }

        #endregion

        #region Editing Of Trainer

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id !";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerToUpdate(id);

            if(trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id , TrainerToUpdateViewModel trainerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(trainerToUpdate);
            }

            var result = _trainerService.UpdateTrainerDetailsById(id,trainerToUpdate);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully . ";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Update  ";
            }
            return RedirectToAction(nameof(Index));

        }



        #endregion

        #region Deleting Of Trainer

        public IActionResult Delete(int id)
        {
            var trainer = _trainerService.GetTrainerDetailsById(id);
            if (trainer is null )
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = trainer.Id;
            return View();
        }

        public IActionResult DeleteConfirmed(int id)
        {
            var result = _trainerService.RemoveTrainerDetailsById(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Trainer";

            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
