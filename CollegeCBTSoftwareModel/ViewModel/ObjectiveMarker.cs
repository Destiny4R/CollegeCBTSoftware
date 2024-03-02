using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class ObjectiveMarker
    {
        public int CourseId { get; set; }
        public int SessionId { get; set; }
        public string Level { get; set; }
        public string RegNumber { get; set; }
        public string Semester { get; set; }
        public ICollection<GetQuestion> GetQuestionz { get; set; }
    }
}
