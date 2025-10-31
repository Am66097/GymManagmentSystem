using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IMemberShipRepository : IGenericRepository<MemberShip>
    {
        IEnumerable<MemberShip> GetAllWithIncludes();
        MemberShip? GetByIdWithIncludes(int id);
    }
}
