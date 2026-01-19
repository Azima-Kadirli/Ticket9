using System.ComponentModel.DataAnnotations;

namespace Ticket9.ViewModel.Employee
{
    public class EmployeeGetVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
    }
}
