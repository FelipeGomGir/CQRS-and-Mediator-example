using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                   new LeaveType
                   {
                       Id = 1,
                       Name = "Vacation",
                       DefaultDays = 10,
                       DateCreated = new DateTime(2025,1,1),
                       DateModified = new DateTime(2025, 1, 1),


                   }
                );
            // This Is a restriction made at DB level.
            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
