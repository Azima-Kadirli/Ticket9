using Ticket9.Models.Common;

namespace Ticket9.Models
{
    public class Employee:BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
