using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace PQT.CC.Models
{
    public class Applicant
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "You are only allow to have upto 50 characters as a name!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please make sure there is no space between characters! (field only supports A-Za-z)")]        
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "You are only allow to have upto 50 characters as a name!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please make sure there is no space between characters! (field only supports A-Za-z)")]        
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]        
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Annual Income")]
        [DataType(DataType.Currency)]
        public double AnnualIncome { get; set; }
    }
}
