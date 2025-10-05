using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Trainer : GymUser
    {
        // Represents [HireDate Property == CreatedAt Of BaseEntity] Of Member Class in Fluent API
        #region Property
        public Specialties Specialties { get; set; }
        #endregion

        #region Relationships

        #region Trainer - Session
        public ICollection<Session> TrainerSessions { get; set; } = null!; 
        #endregion

        
        #endregion



    }
}
