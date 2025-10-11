using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.SessionViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int AvailabaleCapacity { get; set; }

        #region Combuted Properties

        public string DateDisplay => $"{StartDate: MM dd , yyyy}";
        public string TimeRangeDisPlay => $"{StartDate: hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;

        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now)
                    return "UnComing";
                else if (EndDate >= DateTime.Now && StartDate <= DateTime.Now)
                    return "Ongoing";
                else return "Completed";

            }
        }


        #endregion



    }
}
