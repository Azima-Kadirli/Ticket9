using System.ComponentModel.DataAnnotations;

namespace Ticket9.ViewModel.Employee
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string FullName { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
