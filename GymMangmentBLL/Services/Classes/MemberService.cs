using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemberViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _repository;
        private readonly IGenericRepository<MemberShip> memberShipRepo;
        private readonly IPlanRepository planRepository;

        public MemberService(IGenericRepository<Member> memberRepo,
                             IGenericRepository<MemberShip> memberShipRepo,
                             IPlanRepository planRepository)
        {
            _repository = memberRepo;
            this.memberShipRepo = memberShipRepo;
            this.planRepository = planRepository;
        }

        public bool CreateMember(CreateMemberViewModel Createdmember)
        {
            try
            {  //Check If Email Is Exists
                var emailExists = _repository.GetAll(x => x.Email == Createdmember.Email).Any();
                //Check If Phone Is Exists
                var phoneExists = _repository.GetAll(x => x.PhoneNumber == Createdmember.Phone).Any();

                //If One Of Them Exists Return False
                if (emailExists || phoneExists) return false;
                //If Not Add Member And Return True If Added
                var member = new Member()
                {
                    Name = Createdmember.Name,
                    Email = Createdmember.Email,
                    PhoneNumber = Createdmember.Phone,
                    Gender = Createdmember.Gender,
                    DateOfBirth = Createdmember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = Createdmember.BuildingNumber,
                        City = Createdmember.City,
                        Street = Createdmember.Street
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = Createdmember.HealthRecordViewModel.Height,
                        Weight = Createdmember.HealthRecordViewModel.Weight,
                        BloodType = Createdmember.HealthRecordViewModel.BloodType,
                        Notes = Createdmember.HealthRecordViewModel.Note
                    }
                };

                return _repository.Add(member) > 0;
            }
            catch (Exception ) 
            {
            return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _repository.GetAll();
            if(Members == null || !Members .Any()) { return Enumerable.Empty<MemberViewModel>(); }//== []
            var MemberViewModels = Members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone=x.PhoneNumber,
                Photo=x.Photo,
                Gender=x.Gender.ToString()
            });                                                                                      
            return MemberViewModels;
        }

        public MemberViewModel? GetMemberDetailsById(int MemberId)
        {

            var member = _repository.GetById(MemberId);
            if (member == null) return null;
            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.PhoneNumber,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                Address = $"{member.Address.BuildingNumber}, {member.Address.Street}, {member.Address.City}",
                Photo = member.Photo,
            };

            //Active Membership

            var activeMembership = this.memberShipRepo.GetAll(m => m.Id == MemberId && m.Status=="Active")
                .FirstOrDefault();

            if(activeMembership != null)
            {
                memberViewModel.MembershipStartDate= activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate= activeMembership.EndDate.ToShortDateString();

                var plan = planRepository.GetById(activeMembership.PlanId);
                memberViewModel.PlanName = plan?.Name;


            }
            return memberViewModel;

        }
    }
}
