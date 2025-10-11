using AutoMapper;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagmentDAL.Entities;
using GymMangmentBLL.ViewModels.SessionViewModels;
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
        
            CreateMap<Session,SessionViewModel>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                 .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                 .ForMember(dest => dest.AvailabaleCapacity, opt => opt.Ignore());
            
            CreateMap<CreateSessionViewModel, Session>();
       
            CreateMap<Session, UpdateSessionViewModel>(); 
            
            CreateMap<UpdateSessionViewModel, Session>();



        }
        

    }
}
