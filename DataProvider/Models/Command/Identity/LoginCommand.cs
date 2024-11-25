using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Identity
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "لطفا مقدار را وارد کنید.")]
        public string? EmailOrUserName { get; set; }

        [Required(ErrorMessage = "لطفا پسوردتان را وارد کنید.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
