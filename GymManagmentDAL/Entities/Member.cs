using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Member : GymUser
    {
        // Represents [JoinDate Property == CreatedAt Of BaseEntity] Of Member Class in Fluent API

        #region Properties

        public string Photo { get; set; } = null!;

        #endregion

        #region Relationships

        #region Member - HealthRecord
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion


        #region Member - MemberShips

        public ICollection<MemberShip> MemberShips { get; set; } = null!;

        #endregion

        #region Member - MemberSession

        public ICollection<MemberSession> MemberSessions { get; set; }

        #endregion  

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();


        #endregion


    }
}
