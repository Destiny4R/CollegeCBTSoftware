using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeCBTSoftwareWeb.Pages.Manager.Manage_Courses
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public IEnumerable<CourseTable> subject { get; set; }
        public IndexModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            subject = dbContext.CourseTables.ToList();
        }
    }
}
