using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_authorization_crud_4_12_2025.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }    
        public string Email { get; set; }   
        public long? Mobile { get; set; }
        public string Password { get; set; }    
        public string Rolesname { get; set; }
        public bool IsActive { get; set; } 

    }
}
