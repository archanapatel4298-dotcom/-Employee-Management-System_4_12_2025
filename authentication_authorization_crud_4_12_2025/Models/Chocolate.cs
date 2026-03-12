using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_authorization_crud_4_12_2025.Models
{
    [Table("Chocolates")]
    public class Chocolate
    {
        [Key]
        public int ChocolateId { get; set; }
        public string choco_name { get; set; }
        public  string choco_company { get; set; }
        public string choco_type { get; set; }
        public double cost { get; set; }
        public string user { get; set; }

    }
}
