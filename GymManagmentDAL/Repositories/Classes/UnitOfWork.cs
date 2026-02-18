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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;
        private readonly ISessionRepository _sessionRepository1;
        private readonly IMemberShipRepository _memberShipRepository;

        public UnitOfWork(GymDbContext dbContext , ISessionRepository sessionRepository,IMemberShipRepository memberShipRepository)
        {
            _dbContext = dbContext;
            _sessionRepository1 = sessionRepository;
            this._memberShipRepository = memberShipRepository;
            _dbContext = dbContext;
            MemberRepository = new MemberRepository(_dbContext);
            PlanRepository = new PlanRepository(_dbContext);
            SessionRepository = new SessionRepository(_dbContext);
            MemberSessionRepository = new MemberSessionRepository(dbContext);
        }
        public IMemberRepository MemberRepository { get; private set; }
        public IPlanRepository PlanRepository { get; private set; }

        public ISessionRepository SessionRepository { get; private set; }
        public IMemberSessionRepository MemberSessionRepository { get; private set; }

        public IMemberShipRepository MemberShipRepository => _memberShipRepository ?? new MemberShipRepository(_dbContext);

        public ISessionRepository sessionRepository => new SessionRepository(_dbContext);

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            var EntityType = typeof(T);
            if ( _repositories.TryGetValue(EntityType,out var Repo))
                return (IGenericRepository<T>)Repo;

            var NewRepo = new GenericRepository<T>(_dbContext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;

        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
