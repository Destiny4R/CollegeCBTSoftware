using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class StudentCourseReg
    {
        public int Id { get; set; }
        public double? Scores { get; set; }
        public double? BonusScore { get; set; }
        public int CourseId { get; set; }
        public int LevelExamId { get; set; }
        public DateTime? Attendance { get; set; }
        public bool Taken { get; set; } = false;
        [ForeignKey(nameof(LevelExamId))]
        public LevelRegistration LevelExamReg { get; set; }
        [ForeignKey(nameof(CourseId))]
        public CourseTable CourseTable { get; set; }
    }
}
