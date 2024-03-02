using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class StudentTable
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string FullName { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        public int SessionYearId { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey(nameof(SessionYearId))]
        public SessionTable SessionTable { get; set; }

    }
}
