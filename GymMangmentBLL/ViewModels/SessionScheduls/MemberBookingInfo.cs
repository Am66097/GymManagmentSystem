using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionScheduls
{
    public class MemberBookingInfo
    {
        public int MemberSessionId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public bool IsAttended { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
