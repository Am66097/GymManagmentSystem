using AutoMapper;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return Enumerable.Empty<PlanViewModel>();

            #region Before Using AutoMapper Pattern
            //return Plans.Select(p => new PlanViewModel()
            // {
            //     Id = p.Id,
            //     Name = p.Name,
            //     Description = p.Description,
            //     DurationDays = p.DurationInDays,
            //     Price = p.Price,
            //     IsActive = p.IsActive
            // }); 
            #endregion


            return _mapper.Map<IEnumerable<PlanViewModel>>(Plans); // Using AutoMapper Pattern
        }

        public PlanViewModel? GetPlanById(int id)
        {
         
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan == null) return null;
            #region Before Using AutoMapper Pattern
            //return new PlanViewModel()
            //{
            //    Id = plan.Id,
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationInDays,
            //    Price = plan.Price,
            //    IsActive = plan.IsActive
            //}; 
            #endregion

            return _mapper.Map<PlanViewModel>(plan); // Using AutoMapper Pattern
        }


     public   UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || plan.IsActive == false || HasActiveMemberShips(PlanId)) return null;

            #region Before Using AutoMapper Pattern
            //return new UpdatePlanViewModel()
            //{
            //    PlanName = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationInDays,
            //    Price = plan.Price
            //} 
            #endregion;

            return _mapper.Map<UpdatePlanViewModel>(plan); // Using AutoMapper Pattern
        }

     public   bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null || HasActiveMemberShips(PlanId)) return false;
            #region Before Using AutoMapper Pattern
            //( Plan.Description, Plan.DurationInDays, Plan.Price,Plan.UpdatedAt) =
            //    ( UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price ,DateTime.Now); 
            #endregion

            _mapper.Map(UpdatedPlan, Plan); // Using AutoMapper Pattern

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;


            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TogglePlanStatus(int PlanId)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();

            var Plan = Repo.GetById(PlanId);
            if (Plan == null || HasActiveMemberShips(PlanId)) return false;
            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.UpdatedAt = DateTime.Now;
            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper

        private bool HasActiveMemberShips(int planId)
        {
            var activeMemberShips = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(m => m.PlanId == planId && m.Status=="Active");
            return activeMemberShips.Any();
        }
        #endregion
    }
}
