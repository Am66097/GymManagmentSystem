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
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext dbContext=new GymDbContext();
        public int Add(Member member)
        {
            dbContext.Members.Add(member);
            return dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var member = dbContext.Members.Find(Id);
            if (member is null) return 0;

            dbContext.Members.Remove(member);
            return dbContext.SaveChanges();

        }

        public IEnumerable<Member> GetAll() => dbContext.Members.ToList();
       

        public Member? GetById(int Id) => dbContext.Members.Find(Id);
        

        public int Update(Member member)
        {
          dbContext.Members.Update(member); 
            return dbContext.SaveChanges();
        }
    }
}
