using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {

        IEnumerable<T> GetAll(Func<T,bool>? Condition = null);
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
