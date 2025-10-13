using AutoMapper;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagmentDAL.Entities;
using GymMangmentBLL.ViewModels.MemberViewModels;
using GymMangmentBLL.ViewModels.PlanViewModels;
using GymMangmentBLL.ViewModels.SessionViewModels;
using GymMangmentBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {

            #region AutoMappering For Session 
           
            CreateMap<Session, SessionViewModel>()
         .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
         .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
         .ForMember(dest => dest.AvailabaleCapacity, opt => opt.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>();

            CreateMap<UpdateSessionViewModel, Session>();

            #endregion

            #region AutoMappering For Member 
            // GetMemberDetailsById
            CreateMap<Member, MemberViewModel>()
    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
    .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
        $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}"))
    .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
    // القيم دي هنعبيها يدويًا بعد المابّينج
    .ForMember(dest => dest.MembershipStartDate, opt => opt.Ignore())
    .ForMember(dest => dest.MembershipEndDate, opt => opt.Ignore())
    .ForMember(dest => dest.PlanName, opt => opt.Ignore());

            //  من CreateMemberViewModel → Member
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord
                {
                    Height = src.HealthRecordViewModel.Height,
                    Weight = src.HealthRecordViewModel.Weight,
                    BloodType = src.HealthRecordViewModel.BloodType,
                    Notes = src.HealthRecordViewModel.Note
                }));

            //  من Member → MemberViewModel
            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}"));

            //  من Member → MemberToUpdateViewModel
            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            //  من MemberToUpdateViewModel → Member (للتحديث)
            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            //  من HealthRecord → HealthRecordViewModel
            CreateMap<HealthRecord, HealthRecordViewModel>()
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Notes));

            //  من HealthRecordViewModel → HealthRecord
            CreateMap<HealthRecordViewModel, HealthRecord>()
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Note));


            #endregion

            #region AutoMappering For Plan

            //  من Entity إلى ViewModel
            CreateMap<Plan, PlanViewModel>()
                .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src => src.DurationInDays));

            //  من Entity إلى UpdatePlanViewModel
            CreateMap<Plan, UpdatePlanViewModel>()
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src => src.DurationInDays));

            //  من UpdatePlanViewModel إلى Entity (عند التحديث)
            CreateMap<UpdatePlanViewModel, Plan>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PlanName))
                .ForMember(dest => dest.DurationInDays, opt => opt.MapFrom(src => src.DurationDays))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));


            #endregion

            #region AutoMappering For Trainer

            //  من Trainer → TrainerViewModel
            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialties.ToString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.Address.BuildingNumber,
                    Street = src.Address.Street,
                    City = src.Address.City
                }));

            //  من CreateTrainerViewModel → Trainer
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialty))
                .ForMember(dest => dest.Address, opt => opt.Ignore()); // هتتعامل مع الـ Address منفصل لو لزم الأمر

            //  من Trainer → TrainerToUpdateViewModel
            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialties))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            //  من TrainerToUpdateViewModel → Trainer
            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialty))
                .ForMember(dest => dest.Address, opt => opt.Ignore()); // عشان هنحدث العنوان يدويًا جوه السيرفس
            #endregion



        }
        

    }
}
