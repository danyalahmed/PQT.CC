using System;
using System.ComponentModel.DataAnnotations;

namespace PQT.CC.Models
{
    public class Promotion
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
