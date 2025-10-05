using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace GymManagmentDAL.Data.Configurations
{
    internal class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
           builder.ToTable(tb =>
           {
               tb.HasCheckConstraint("SessionVaildDayCheck", "Capacity between 1 and 25");
               tb.HasCheckConstraint("SessionVaildEndTimeCheck", "EndTime > StartTime");
           });

            builder.HasOne(x => x.SessionCategory)
                .WithMany(x => x.Sessions)
                .HasForeignKey(X => X.CategoryId);

            builder.HasOne(x => x.SessionTrainer)
               .WithMany(x => x.TrainerSessions)
               .HasForeignKey(X => X.TrainerId);
        }
    }
}
