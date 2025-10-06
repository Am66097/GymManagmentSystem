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
    internal class CategoryRepositories : ICateoryRepository
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        public int Add(Category category)
        {
            dbContext.Categories.Add(category);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var category = dbContext.Categories.Find(id);
            if (category is null) return 0;

            dbContext.Categories.Remove(category);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => dbContext.Categories.ToList();

        public Category? GetById(int id) => dbContext.Categories.Find(id);

        public int Update(Category category)
        {
            dbContext.Categories.Update(category);
            return dbContext.SaveChanges();
        }
    }
}
