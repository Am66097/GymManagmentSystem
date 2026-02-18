using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionScheduls
{
    public class MemberSessionViewModel
    {
        public int Id { get; set; }

        public string SessionName { get; set; } = null!;

        public string TrainerName { get; set; } = null!;

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration => EndTime - StartTime;

        public int Capacity { get; set; }

        public int BookedSlots { get; set; }

        public string Status { get; set; } = null!; 

        public string Description { get; set; } = null!;
    }

}
