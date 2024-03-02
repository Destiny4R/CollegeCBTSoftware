using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class ExamReady
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Semester { get; set; }
        public string Level { get; set; }
        public int CourseId { get; set; }
        public int SessionYearId { get; set; }
        public int NoToAnswer { get; set; }
        public DateTime StartDate { get; set; }
        public double Duration { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ForeignKey(nameof(SessionYearId))]
        public SessionTable SessionTable { get; set; }
        [ForeignKey(nameof(CourseId))]
        public CourseTable CourseTable { get; set; }
        public bool PublishExam { get; set; } = true;
    }
}
