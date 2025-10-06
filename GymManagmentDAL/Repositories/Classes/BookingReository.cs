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
    internal class BookingReository : IBookingRepository
    {
        private readonly GymDbContext dbContext = new GymDbContext();

        public int Add(Bookings bookings)
        {
            dbContext.Bookings.Add(bookings);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var bookings = dbContext.Bookings.Find(id);
            if (bookings is null) return 0;

            dbContext.Bookings.Remove(bookings);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Bookings> GetAll() => dbContext.Bookings.ToList();

        public Bookings? GetById(int id) => dbContext.Bookings.Find(id);

        public int Update(Bookings bookings)
        {
            dbContext.Bookings.Update(bookings);
            return dbContext.SaveChanges();
        }
    }
}
