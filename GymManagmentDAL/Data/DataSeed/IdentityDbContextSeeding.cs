using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
			try
			{
                var hasUsers = userManager.Users.Any();
                var hasRoles = roleManager.Roles.Any();
                if (hasUsers && hasRoles) return false;

                if(!hasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new(){Name="SuperAdmin"},
                        new(){Name="Admin"}
                    };

                    foreach (var Role in Roles)
                    {
                        if(!roleManager.RoleExistsAsync(Role.Name!).Result)
                        {
                            roleManager.CreateAsync(Role).Wait();
                        }
                    }
                }

                if(!hasUsers)
                {
                    var mainAdmin = new ApplicationUser()
                    {
                        FirstName = "Amin",
                        LastName = "Ashraf",
                        UserName = "AminAshraf",
                        Email = "AminAshraf@gmail.com",
                        PhoneNumber = "01123953371"
                    };
                    userManager.CreateAsync(mainAdmin,"P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(mainAdmin,"SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Mohamed",
                        LastName = "Ashraf",
                        UserName = "MohamedAshraf",
                        Email = "MohamedAshraf@gmail.com",
                        PhoneNumber = "01523953371"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();

                    
                }

                return true;

			}
			catch (Exception ex)
			{
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
			}
        }

    }
}
