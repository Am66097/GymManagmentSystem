using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class HealthRecord : BaseEntity
    {

        public int Height { get; set; } 
        public int Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Notes { get; set; }

        // Represents [LastUpdate Property == UpdatedAt Of BaseEntity] Of Member Class in Fluent API

    }
}
