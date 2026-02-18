using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GymManagmentDAL.Repositories.Classes
{
    public class MemberSessionRepository : GenericRepository<MemberSession>, IMemberSessionRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberSessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberSession> GetBySessionId(int sessionId)
        {
            return _dbContext.MemberSessions
                             .Include(ms => ms.Member)
                             .Where(ms => ms.SessionId == sessionId)
                             .ToList();
        }

      

        public IEnumerable<MemberSession> GetAllWithMemberAndSession()
        {
            return _dbContext.MemberSessions
                           .Include(ms => ms.Member)
                           .Include(ms => ms.Session)
                           .AsNoTracking()
                           .ToList();
        }
    }
}
