using System.ComponentModel.DataAnnotations;

namespace Ticket9.ViewModel.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
