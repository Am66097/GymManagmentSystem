using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Member : GymUser
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

        #region Member - MemberSessions

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;

        #endregion

        #endregion

        //public int PlanId { get; set; } // مش عارف مكانها هنا صح ولا لاء ,دي انا ضايفها مؤاخرا لحل خطأ تاني قدام , يعني مش ضايفها وقت انشاء الكلاسات

    }
}
