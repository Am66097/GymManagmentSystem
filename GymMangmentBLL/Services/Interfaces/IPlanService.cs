using GymMangmentBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface IPlanService
    {
        public IEnumerable<PlanViewModel> GetAllPlans();

        public PlanViewModel? GetPlanById(int id);

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId);

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan);
        public bool TogglePlanStatus(int PlanId);
    }
}
