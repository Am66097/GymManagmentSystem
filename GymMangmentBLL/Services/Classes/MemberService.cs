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
        #region Before Useing Unit Of Work Pattern
        //private readonly IGenericRepository<Member> _memberRepository;
        //private readonly IGenericRepository<MemberShip> _memberShipRepo;
        //private readonly IPlanRepository _planRepository;
        //private readonly IGenericRepository<HealthRecord> _healthRecordRepo;
        //private readonly IGenericRepository<MemberSession> _memberSessionRepo; 

        //public MemberService(IGenericRepository<Member> memberRepo,
        //                     IGenericRepository<MemberShip> memberShipRepo,
        //                     IPlanRepository planRepository,
        //                     IGenericRepository<HealthRecord> healthRecordRepo,
        //                     IGenericRepository<MemberSession> memberSessionRepo)
        //{
        //    _memberRepository = memberRepo;
        //    _memberShipRepo = memberShipRepo;
        //    _planRepository = planRepository;
        //    _healthRecordRepo = healthRecordRepo;
        //    _memberSessionRepo = memberSessionRepo;
        //}
        #endregion


        #region After Useing Unit Of Work Pattern

        private readonly IUnitOfWork _unitOfWork;        

        public MemberService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }


	    #endregion

        public bool CreateMember(CreateMemberViewModel Createdmember)
        {
            try
            {
                ////Check If Email Is Exists
                //var emailExists = _memberRepository.GetAll(x => x.Email == Createdmember.Email).Any();
                ////Check If Phone Is Exists
                //var phoneExists = _memberRepository.GetAll(x => x.PhoneNumber == Createdmember.Phone).Any();

                ////If One Of Them Exists Return False
                //if (emailExists || phoneExists) return false; // ==

                if (IsEmailExists(Createdmember.Email) || IsPhoneExists(Createdmember.Phone)) return false;



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

                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) { return Enumerable.Empty<MemberViewModel>(); }//== []
            var MemberViewModels = Members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.PhoneNumber,
                Photo = x.Photo,
                Gender = x.Gender.ToString()
            });
            return MemberViewModels;
        }

        public MemberViewModel? GetMemberDetailsById(int MemberId)
        {

            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
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

            var activeMembership = _unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Id == MemberId && m.Status == "Active")
                .FirstOrDefault();

            if (activeMembership != null)
            {   
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);
                memberViewModel.PlanName = plan?.Name;


            }
            return memberViewModel;

        }

        public HealthRecordViewModel GetHealthRecordDetailsById(int MemberId)
        {
            var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (healthRecord == null) return null;
            var healthRecordViewModel = new HealthRecordViewModel
            {
                Height = healthRecord.Height,
                Weight = healthRecord.Weight,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Notes
            };
            return healthRecordViewModel;
        }

        public MemberToUpdateViewModel GetMemberToUpdateById(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.PhoneNumber,
                Photo = member.Photo,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
                DateOfBirth = member.DateOfBirth

            };
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                ////Check If Email Is Exists
                //var emailExists = _memberRepository.GetAll(x => x.Email == UpdatedMember.Email && x.Id != MemberId).Any();
                ////Check If Phone Is Exists
                //var phoneExists = _memberRepository.GetAll(x => x.PhoneNumber == UpdatedMember.Phone && x.Id != MemberId).Any();
                ////If One Of Them Exists Return False
                //if (emailExists || phoneExists) return false; // ==

                if (IsEmailExists(UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone)) return false;

                var Repo = _unitOfWork.GetRepository<Member>();

                var oldMember = Repo.GetById(MemberId);
                if (oldMember == null) return false;
                oldMember.Name = UpdatedMember.Name;
                oldMember.Email = UpdatedMember.Email;
                oldMember.PhoneNumber = UpdatedMember.Phone;
                oldMember.Photo = UpdatedMember.Photo;
                oldMember.DateOfBirth = UpdatedMember.DateOfBirth;
                oldMember.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                oldMember.Address.Street = UpdatedMember.Street;
                oldMember.Address.City = UpdatedMember.City;
                oldMember.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0 ;
                
            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool RemoveMember(int MemberId)
        {
            var Repo = _unitOfWork.GetRepository<Member>();
            var RepoMemberShip = _unitOfWork.GetRepository<MemberShip>();

            var member = Repo.GetById(MemberId);
            if (member == null) return false;

            var HasActiveMembership = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(x => x.MemberId == MemberId && x.Session.StartDate > DateTime.Now).Any();

            if (HasActiveMembership) return false;
            var MemberShips = RepoMemberShip.GetAll(x => x.MemberId == MemberId);

            try
            {

                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                    {
                        RepoMemberShip.Delete(memberShip);
                    }

                }
                 Repo.Delete(member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #region Helper Methods
        
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.PhoneNumber == phone).Any();
        }


        #endregion




    }
}
