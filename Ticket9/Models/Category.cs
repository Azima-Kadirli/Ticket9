using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Ticket9.Models.Common;

namespace Ticket9.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
