using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket9.Models;

namespace Ticket9.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Image).IsRequired();
            builder.HasOne(e => e.Category).WithMany(e => e.Employees).HasForeignKey(e => e.CategoryId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
