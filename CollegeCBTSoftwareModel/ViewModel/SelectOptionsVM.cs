using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareModel.ViewModel
{
    public class SelectOptionsVM
    {
        public List<SelectListItem> _Sessions { get; set; }
        public List<SelectListItem> _Course { get; set; }
    }
}
