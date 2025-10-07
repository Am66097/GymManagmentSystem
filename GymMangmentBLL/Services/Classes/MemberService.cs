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
        public MemberService(IGenericRepository<Member> memberRepo)
        {
            _repository = memberRepo;
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
    }
}
