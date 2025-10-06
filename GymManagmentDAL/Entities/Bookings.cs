using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Bookings : BaseEntity
    {
        public int MemberId { get; set; }

        public int SessionId { get; set; }

        public DateTime BookingDate { get; set; }

        public bool IsAttended { get; set; }

        //public Member Member { get; set; }

        //public Session Session { get; set; }
    }
}
