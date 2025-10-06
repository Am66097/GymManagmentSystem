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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        public int Add(Plan plan)
        {
            dbContext.Plans.Add(plan);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = dbContext.Plans.Find(id);
            if (plan is null) return 0;

            dbContext.Plans.Remove(plan);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => dbContext.Plans.ToList();

        public Plan? GetById(int id) => dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            dbContext.Plans.Update(plan);
            return dbContext.SaveChanges();
        }
    }
}
