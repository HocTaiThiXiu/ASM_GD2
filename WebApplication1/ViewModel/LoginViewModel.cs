using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
