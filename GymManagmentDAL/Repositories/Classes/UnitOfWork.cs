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
        private readonly ISessionRepository sessionRepository1;

        public UnitOfWork(GymDbContext dbContext , ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            sessionRepository1 = sessionRepository;
        }

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
