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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        public int Add(Trainer trainer)
        {
            dbContext.Trainers.Add(trainer);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var trainer = dbContext.Trainers.Find(id);
            if (trainer is null) return 0;

            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => dbContext.Trainers.ToList();

        public Trainer? GetById(int id) => dbContext.Trainers.Find(id);

        public int Update(Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();
        }
    }
}
