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
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext gymDbContext;

        public PlanRepository(GymDbContext gymDbContext) 
        {
            this.gymDbContext = gymDbContext;
        }
        public IEnumerable<Plan> GetAll() => this.gymDbContext.Plans.ToList();



        public Plan? GetById(int id) => this.gymDbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            this.gymDbContext.Set<Plan>().Update(plan);
            return this.gymDbContext.SaveChanges();
        }
    }
}
