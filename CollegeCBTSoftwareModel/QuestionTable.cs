using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class QuestionTable
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? Question { get; set; }
        [StringLength(500)]
        public string? OptionOne { get; set; }
        [StringLength(500)]
        public string? OptionTwo { get; set; }
        [StringLength(500)]
        public string? OptionThree { get; set; }
        [StringLength(500)]
        public string? OptionFour { get; set; }
        [StringLength(500)]
        public string Answer { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public CourseTable CourseTable { get; set; }

    }
}
