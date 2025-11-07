using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class MemberShip : BaseEntity
    {
        // StartDate == CreatedAt

        #region Properties
        public DateTime EndDate { get; set; }

        public string Status
        { 
        get
            {

                if (EndDate <= DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }
        #endregion

        #region Relationships

        #region Membership - Member
        public Member Member { get; set; } 
        public int MemberId { get; set; }
        #endregion

        #region Membership - Plan
        public Plan Plan { get; set; }
        public int PlanId { get; set; }  
        #endregion

        #endregion

    }
}
