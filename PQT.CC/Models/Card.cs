using System.ComponentModel.DataAnnotations;

namespace PQT.CC.Models
{
    public class Card
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double APR { get; set; }
        //there might not be any!!
        public Promotion Promotion { get; set; }
    }
}
