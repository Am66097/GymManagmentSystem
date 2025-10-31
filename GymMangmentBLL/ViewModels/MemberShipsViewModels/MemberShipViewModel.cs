using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.MemberShipsViewModels
{
    public class MemberShipViewModel
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Extra display info
        public string MemberName { get; set; } = null!;
        public string PlanName { get; set; } = null!;
    }
}
