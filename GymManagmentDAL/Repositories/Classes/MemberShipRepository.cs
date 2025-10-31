using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class MemberShipRepository : GenericRepository<MemberShip>, IMemberShipRepository
    {
        private readonly GymDbContext _context;

        public MemberShipRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<MemberShip> GetAllWithIncludes()
        {
            return _context.MemberShips
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .ToList();
        }

        public MemberShip? GetByIdWithIncludes(int id)
        {
            return _context.MemberShips
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
