using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository sessionRepository { get; }
        public IMemberShipRepository MemberShipRepository { get; }
        IPlanRepository PlanRepository { get; }
        IMemberRepository MemberRepository { get; }
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity, new();

        int SaveChanges();

    }
}
