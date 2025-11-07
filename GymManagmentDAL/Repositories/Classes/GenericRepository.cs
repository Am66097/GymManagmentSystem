
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymManagmentDAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T>
     where T : BaseEntity, new()

    {
        private readonly GymDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GymDbContext context)
        {
            _dbcontext = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);

        public void Delete(T entity)
        {
            var entry = _dbcontext.Entry(entity);
            if (entry.State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(Func<T, bool>? condition = null)
        {
            return condition == null
                ? _dbSet.AsNoTracking().ToList()
                : _dbSet.AsNoTracking().Where(condition).ToList();
        }

        public T? GetById(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public void Update(T entity)
        {
            var entry = _dbcontext.Entry(entity);

            if (entry.State == EntityState.Detached)
                _dbSet.Attach(entity);

            entry.State = EntityState.Modified;
        }

       

    }

}
