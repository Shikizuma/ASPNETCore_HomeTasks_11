using System.ComponentModel.DataAnnotations;

namespace ASPNETCore_HomeTasks_11.Models
{
    public class LoginModel
    {
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Password")]
        [UIHint("Password")]
        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
