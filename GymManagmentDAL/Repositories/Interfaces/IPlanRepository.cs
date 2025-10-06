using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int Id);
        int Add(Plan plan);
        int Update(Plan plan);
        int Delete(int Id);
    }
}
