using AutoMapper;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemberShipsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class MemberShipService : IMemberShipService
    {
        private readonly IMemberShipRepository _memberShipRepo;

        public MemberShipService(IMemberShipRepository memberShipRepo)
        {
            _memberShipRepo = memberShipRepo;
        }

        public IEnumerable<MemberShip> GetAll()
        {
            return _memberShipRepo.GetAllWithIncludes();
        }

        public MemberShip? GetById(int id)
        {
            return _memberShipRepo.GetByIdWithIncludes(id);
        }

        public void Add(MemberShip entity)
        {
            _memberShipRepo.Add(entity);
        }

        public void Update(MemberShip entity)
        {
            _memberShipRepo.Update(entity);
        }

        public void Delete(int id)
        {
            var membership = _memberShipRepo.GetById(id);
            if (membership != null)
                _memberShipRepo.Delete(membership);
        }
    }
}
