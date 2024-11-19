using System.ComponentModel.DataAnnotations;

namespace Base.Models.Command.Identity
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "لطفا ایمیلتان را وارد کنید.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "لطفا پسوردتان را وارد کنید.")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
