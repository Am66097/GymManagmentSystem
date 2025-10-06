using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IBookingRepository
    {
        int Add(Bookings booking);
        int Update(Bookings booking);
        int Delete(int id);
        Bookings? GetById(int id);
        IEnumerable<Bookings> GetAll();
    }
}
