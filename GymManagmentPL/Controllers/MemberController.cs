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
            } //Validation ده هو هو نفس الكومنت اللي تحت بس عليه 

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

        #region Edit Member 

        public ActionResult MemberEdit(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberservice.GetMemberToUpdateById(id);

            if(Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(Member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id,MemberToUpdateViewModel MemberToEdit)
        {
            if(!ModelState.IsValid) 
                return View(MemberToEdit);

            var Result = _memberservice.UpdateMemberDetails(id, MemberToEdit);
            if(Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Updated ";

            }
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region Delete Member

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberservice.GetMemberDetailsById(id);
            if(Member is null)
            {
                TempData["ErrorMessage"] = "Memeber Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm]int id)
        {
            var result = _memberservice.RemoveMember(id);
            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            else
                TempData["SuccessMessage"] = "Failed To Delete Member ";
            return RedirectToAction(nameof(Index));

        }

        #endregion




    }
}
