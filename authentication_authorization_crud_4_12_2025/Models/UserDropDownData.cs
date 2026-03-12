using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_authorization_crud_4_12_2025.Models
{
    [Table("UserDropDownData")]
    public class UserDropDownData
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
