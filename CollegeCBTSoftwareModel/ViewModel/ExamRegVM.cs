using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class ExamRegVM
    {
        [ValidateNever]
        public int Id { get; set; }
        [ValidateNever]
        public int StudentId { get; set; }
        [Required]
        public string Semester { get; set; }
        [Display(Name = "Class")]
        [Required]
        public string Level { get; set; }
        [Display(Name = "Seesion/Year")]
        [Required]
        public int SessionYearId { get; set; }
        public List<int> Courses { get; set; }
        [ValidateNever]
        public ICollection<StudentCourseReg> StudentCourses { get; set; }
        [ValidateNever]
        public StudentTable Student { get; set; }
    }
}
