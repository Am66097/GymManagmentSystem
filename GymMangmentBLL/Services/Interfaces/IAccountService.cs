using GymManagmentDAL.Entities;
using GymMangmentBLL.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? validateUser(LoginViewModel loginViewModel);

    }
}
