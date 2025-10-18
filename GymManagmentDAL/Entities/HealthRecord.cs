using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class HealthRecord : BaseEntity
    {
        // Represents [LastUpdate Property == UpdatedAt Of BaseEntity] Of Member Class in Fluent API

        #region Properties
        [Column(TypeName = "decimal(5,2)")]
        public decimal Height { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Notes { get; set; }
        #endregion

        



    }
}
