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
        public IActionResult Index()
        {
            var members = _memberservice.GetAllMembers();
            return View(members);
        }
    }
}
