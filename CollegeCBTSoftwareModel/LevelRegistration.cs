using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class LevelRegistration
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [StringLength(10)]
        public string Semester { get; set; }
        public string Level { get; set; }
        public int SessionYearId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ForeignKey(nameof(SessionYearId))]
        public SessionTable SessionTable { get; set; }
        [ForeignKey(nameof(StudentId))]
        public StudentTable StudentTable { get; set; }
        public ICollection<StudentCourseReg> StudentCourseReg { get; set; }
    }
}
