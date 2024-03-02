using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel
{
    public class SessionTable
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
