using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IMemberSessionRepository
    {

        int Add(MemberSession memberSession);
        int Update(MemberSession memberSession);
        int Delete(int id);
        MemberSession? GetById(int id);
        IEnumerable<MemberSession> GetAll();
    }
}
