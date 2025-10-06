using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAll();
        Trainer? GetById(int Id);
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(int Id);
    }
}
