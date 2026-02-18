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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainersAndCategory()
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer)
                                      .Include(x=>x.SessionCategory)
                                      .ToList();
        }

        public Session? GetAllSessionsWithTrainersAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer)
                                      .Include(x => x.SessionCategory)
                                      .FirstOrDefault(X=>X.Id == sessionId);
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(X=>X.SessionId == sessionId);

        }
        public IEnumerable<Session> GetAllSessionsWithDetails()
        {
            return _dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .Include(s => s.SessionMembers)
                    .ThenInclude(sm => sm.Member)
                .ToList();
        }

    }
}
