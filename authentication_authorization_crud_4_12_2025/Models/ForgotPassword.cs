using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_crud_4_12_2025.Models
{
    public class ForgotPassword
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
