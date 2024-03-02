using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class ExamsReadyVM
    {
        [ValidateNever]
        public int Id { get; set; }
        [MaxLength(10)]
        [Required]
        public string Semester { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int SessionYearId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name ="Examination Duration")]
        public double Duration { get; set; }
        [Required]
        [Display(Name = "Total Questions")]
        public int NoToAnswer { get; set; }
    }
}
