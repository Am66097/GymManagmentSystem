using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Plan : BaseEntity
    {
        // Represents [StartDate Property == CreatedAt Of BaseEntity] Of Member Class in Fluent API
        #region Properties

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }

        #endregion

        #region Relationships

        #region Plan - MemberShip
        public ICollection<MemberShip> PlanMembers { get; set; } = null!;
        #endregion

        #endregion
    }
}
