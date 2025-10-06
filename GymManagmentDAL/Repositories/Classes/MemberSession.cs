using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    internal class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        public int Add(MemberSession memberSession)
        {
            dbContext.MemberSessions.Add(memberSession);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var memberSession = dbContext.MemberSessions.Find(id);
            if (memberSession is null) return 0;

            dbContext.MemberSessions.Remove(memberSession);
            return dbContext.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAll() => dbContext.MemberSessions.ToList();

        public MemberSession? GetById(int id) => dbContext.MemberSessions.Find(id);

        public int Update(MemberSession memberSession)
        {
            dbContext.MemberSessions.Update(memberSession);
            return dbContext.SaveChanges();
        }
    }
}
