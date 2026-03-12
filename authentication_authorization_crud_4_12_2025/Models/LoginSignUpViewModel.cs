using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_crud_4_12_2025.Models
{
    public class LoginSignUpViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public bool IsRemember { get; set; }
        [Display(Name = "Remember Me")]

        // public string Rolesname { get; set; }
        public bool IsActive { get; set; }
    }
}
