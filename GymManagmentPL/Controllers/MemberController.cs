using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;



namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberservice;

        public MemberController(IMemberService memberservice)
        {
            this._memberservice = memberservice;
        }

        #region Get All Members
        public ActionResult Index()
        {
            var members = _memberservice.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Details 

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var Member = _memberservice.GetMemberDetailsById(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(Member);

        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var HealthRecord = _memberservice.GetHealthRecordDetailsById(id);
            if (HealthRecord == null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);

        }
        #endregion

        #region Create Member 
        //[HttpGet] // Default
        public ActionResult Create()
        {
             
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            
            Console.WriteLine("=== ENTERED CreateMember POST ===");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("=== MODEL STATE INVALID ===");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }

                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedMember);
            }

            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("DataInvalid", " Check Data And Missing Fields ");
            //    return View(nameof(Create), CreatedMember);
            //}

            bool Result = _memberservice.CreateMember(CreatedMember);
            if(Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Create , Check Phone And Email ";
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion




    }
}
