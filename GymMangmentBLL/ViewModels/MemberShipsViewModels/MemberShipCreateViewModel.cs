using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.MemberShipsViewModels
{
    public class MemberShipCreateViewModel
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } 

        // ComboBox Data
        public IEnumerable<SelectListItem>? Members { get; set; }
        public IEnumerable<SelectListItem>? Plans { get; set; }
    }
    

}
