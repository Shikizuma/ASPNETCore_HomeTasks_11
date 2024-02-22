using System.ComponentModel.DataAnnotations;

namespace ASPNETCore_HomeTasks_11.Models
{
    public class RegisterModel
    {
        [Display(Name = "Login")]
        [MaxLength(80)]
        [MinLength(3)]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Name")]
        [MaxLength(80)]
        [MinLength(3)]
        public string Name { get; set; }
        [Display(Name = "Password")]
        [UIHint("Password")]
        [MaxLength(100)]
        [MinLength(6)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [UIHint("Password")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }       
    }
}
