using GymManagmentDAL.Entities;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public ApplicationUser? validateUser(LoginViewModel loginViewModel)
        {
            var user=_userManager.FindByEmailAsync(loginViewModel.Email.ToLower()).Result;

            if (user is null) return null;

            var isPasswordValid = _userManager.CheckPasswordAsync(user,loginViewModel.Password).Result;
            return isPasswordValid ? user : null;
        
        }
    }
}
