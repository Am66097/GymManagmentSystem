using GymManagmentDAL.Entities;
using GymMangmentBLL.ViewModels.MemberShipsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface IMemberShipService
    {
        IEnumerable<MemberShip> GetAll();
        MemberShip? GetById(int id);
        void Add(MemberShip entity);
        void Update(MemberShip entity);
        void Delete(int id);

    }
}
