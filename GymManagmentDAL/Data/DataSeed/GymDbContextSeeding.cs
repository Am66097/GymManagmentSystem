using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {
        public static bool seedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var plans = LoadDataFromFiles<Plan>("plans.json");
                    if (plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                }

                if (!HasCategories)
                {
                    var categories = LoadDataFromFiles<Category>("Categories.json");
                    if (categories.Any())
                    {
                        dbContext.Categories.AddRange(categories);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Faild : {ex}");
                return false;
            }
                return dbContext.SaveChanges() > 0;

        }

        private static List<T> LoadDataFromFiles<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName); 
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            string jsonData = File.ReadAllText(filePath);
            var Options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(jsonData, Options) ?? new List<T>();
        }
    }
}
