using GymMangmentBLL.Services.Interfaces;
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
        if(id<=0)
                return RedirectToAction(nameof(Index));

        var Member = _memberservice.GetMemberDetailsById(id);
            if(Member == null) 
                return RedirectToAction(nameof(Index));
            
            return View(Member);

        }
        #endregion


    }
}
