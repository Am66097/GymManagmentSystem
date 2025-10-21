using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        //[Required(ErrorMessage ="Plan Name Is Requierd")]
        //[StringLength(50,ErrorMessage ="Plan Name Must Be Less Than 51 Char")]
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = " Description Is Requierd")]
        [StringLength(50, ErrorMessage = " Description Must Be Less Than 501 Char")] 
        public string Description { get; set; } = null!;

        [Required(ErrorMessage ="Duration Days Is Required")]
        [Range(1, 366, ErrorMessage = "Duration Days Must Be Between 1 And 365 Days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage ="Price Is Required")]
        [Range(1, 10000, ErrorMessage = "Price Must Be Between 1 And 10000")]
        public decimal Price { get; set; }
    }
}
