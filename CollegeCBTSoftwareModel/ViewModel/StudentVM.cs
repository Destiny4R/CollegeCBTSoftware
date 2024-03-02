using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class StudentVM
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int SessionYearId { get; set; }
        [ValidateNever]
        public string Password { get; set; }
    }
}
