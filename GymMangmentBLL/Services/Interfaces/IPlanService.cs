using GymMangmentBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel>  GetAllPlans();

        PlanViewModel? GetPlanById(int id);

        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);

        bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan);
        bool TogglePlanStatus(int PlanId);
    }
}
