using System.ComponentModel.DataAnnotations;

namespace Auth.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
