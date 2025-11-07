using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class MemberSession : BaseEntity
    {
        // Booking Date == creatededAt

        public string Status { get; set; } = null!;
       
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public bool IsAttended { get; set; } = false;
        public DateTime? AttendanceDate { get; set; }
    }
}
