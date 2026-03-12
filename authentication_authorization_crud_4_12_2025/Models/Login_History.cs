using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_crud_4_12_2025.Models
{
    public class Login_History
    {
        [Key]
        public int Id { get; set; }
        public string UserLoggedin { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastLogout { get; set; }
    }
}
