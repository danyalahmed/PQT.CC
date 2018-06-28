using System;
using System.ComponentModel.DataAnnotations;

namespace PQT.CC.Models
{
    public class Results
    {
        [Key]
        public int Id { get; set; }
        public Applicant Applicant { get; set; }
        public bool IsFailled { get; set; }
        public Card Card { get; set; }
        public DateTime Date { get; set; }
    }
}
