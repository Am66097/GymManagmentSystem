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

        public string Photo { get; set; } = null!;
    }
}
