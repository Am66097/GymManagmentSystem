using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionScheduls
{

    public class CreateBookingViewModel
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; } = string.Empty;

        public int MemberId { get; set; }
        public int AvailableSlots { get; set; }

        public DateTime Date { get; set; }

        // قائمة الأعضاء للـ dropdown
        public List<SelectListItem> Members { get; set; } = new();
    }

}

