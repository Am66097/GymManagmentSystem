using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class HealthRecordConfig : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {

            builder.ToTable("Members")
                .HasKey(hr => hr.Id);

            // One-to-one with HealthRecord
            builder.HasOne<Member>()
                  .WithOne(x => x.HealthRecord)
                  .HasForeignKey<HealthRecord>(X => X.Id)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt); // by default it will be determined by Ef core




        }
    }
}
