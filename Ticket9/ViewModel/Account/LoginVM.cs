using System.ComponentModel.DataAnnotations;

namespace Ticket9.ViewModel.Account
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
