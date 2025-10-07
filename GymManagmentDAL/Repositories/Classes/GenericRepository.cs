using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly GymDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GymDbContext context)
        {
            _dbcontext = context;
            _dbSet = context.Set<T>();
        }

        public int Add(T entity)
        {
           this._dbcontext.Set<T>().Add(entity);
            return this._dbcontext.SaveChanges();
        }

        public int Delete(T entity)
        {
            this._dbcontext.Set<T>().Remove(entity);
            return this._dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll(Func<T, bool>? Condition = null)
        {
            if(Condition is null)
                return _dbcontext.Set<T>().AsNoTracking().ToList();
            else
                return _dbcontext.Set<T>().AsNoTracking().Where(Condition).ToList();
        }

        public T? GetById(int id) =>this._dbcontext.Set<T>().Find(id);

        public int Update(T entity)
        {
            this._dbcontext.Set<T>().Update(entity);
            return this._dbcontext.SaveChanges();
        }
    }
}
