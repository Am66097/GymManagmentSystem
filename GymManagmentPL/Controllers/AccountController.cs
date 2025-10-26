using GymManagmentDAL.Entities;
using GymMangmentBLL.Services.Classes;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService , SignInManager<ApplicationUser> signInManager)
        {
            this._accountService = accountService;
            this._signInManager = signInManager;
        }

        #region Login Action

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user= _accountService.validateUser(model);
            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invaild Email Or Password");
                return View(model);
            }

            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
            if(result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed");
            if(result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out");
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);



        }

        #endregion

        #region Logout Action

        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        #endregion


        #region AccessDenied Action 

        public ActionResult AccessDenied()
        {
            
            return View(); 
        }

        #endregion

    }
}
