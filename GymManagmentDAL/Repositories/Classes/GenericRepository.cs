//using GymManagmentDAL.Data.Context;
//using GymManagmentDAL.Entities;
//using GymManagmentDAL.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GymManagmentDAL.Repositories.Classes
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
//    {
//        private readonly GymDbContext _dbcontext;
//        private readonly DbSet<T> _dbSet;

//        public GenericRepository(GymDbContext context)
//        {
//            _dbcontext = context;
//            _dbSet = context.Set<T>();
//        }

//        public void Add(T entity) => this._dbcontext.Set<T>().Add(entity);


//        public void Delete(T entity) => this._dbcontext.Set<T>().Remove(entity);

//        public IEnumerable<T> GetAll(Func<T, bool>? Condition = null)
//        {
//            if(Condition is null)
//                return _dbcontext.Set<T>().AsNoTracking().ToList();
//            else
//                return _dbcontext.Set<T>().AsNoTracking().Where(Condition).ToList();
//        }

//        public T? GetById(int id) =>this._dbcontext.Set<T>().Find(id);

//        public void Update(T entity) => this._dbcontext.Set<T>().Update(entity);

//    }
//}
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
