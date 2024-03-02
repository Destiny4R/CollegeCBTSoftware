using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class QuestionVM
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? Question { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Option One")]
        public string? OptionOne { get; set; }
        [MaxLength(500)]
        [Required]
        [Display(Name = "Option Two")]
        public string? OptionTwo { get; set; }
        [MaxLength(500)]
        [Display(Name = "Option Three")]
        [ValidateNever]
        public string? OptionThree { get; set; }
        [MaxLength(500)]
        [Display(Name = "Option Four")]
        [ValidateNever]
        public string? OptionFour { get; set; }
        [MaxLength(500)]
        [Required]
        public string? Answer { get; set; }
        [Required]
        [Display(Name = "subject")]
        public int Course { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string Semester { get; set; }
    }
}
