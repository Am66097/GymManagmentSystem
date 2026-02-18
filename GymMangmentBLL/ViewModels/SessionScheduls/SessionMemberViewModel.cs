using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionScheduls
{
    public class SessionMembersViewModel
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public List<MemberBookingInfo> Members { get; set; } = new();

        public string TrainerName { get; set; } = string.Empty;
    }
}
