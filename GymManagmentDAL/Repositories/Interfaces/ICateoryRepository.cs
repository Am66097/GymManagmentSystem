using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ICateoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int Id);
        int Add(Category category);
        int Delete(int Id);
    }
}
