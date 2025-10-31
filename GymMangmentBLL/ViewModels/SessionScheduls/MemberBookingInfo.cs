using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionScheduls
{
    public class MemberBookingInfo
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
    }
}
