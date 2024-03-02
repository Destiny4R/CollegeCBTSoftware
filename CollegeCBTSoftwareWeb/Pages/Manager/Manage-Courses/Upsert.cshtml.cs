using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeCBTSoftwareWeb.Pages.Manager.Manage_Courses
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        [BindProperty]
        public CourseTable course { get; set; }
        public UpsertModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int? id)
        {
            if (id > 0)
            {
                var ses = dbContext.CourseTables.Find(id);
                if (ses != null)
                {
                    course = ses;
                    return Page();
                }
            }
            course = new CourseTable();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (course.Name != null)
            {
                if (course.Id > 0)
                {
                    var classNSub = dbContext.CourseTables.FirstOrDefault(k=>k.Id==course.Id);
                    if (classNSub != null)
                    {
                        classNSub.Name = course.Name;
                        dbContext.Update(classNSub);
                        dbContext.SaveChanges();
                        TempData["success"] = "course Successfully Updated!";
                        return RedirectToPage("Index");
                    }
                }
                CourseTable coursez = new()
                {
                    Name = course.Name
                };
                dbContext.CourseTables.Add(coursez);
                int result = 0;
                try
                {
                    result = dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Course Already Added!";
                }
                if (result > 0)
                {
                    TempData["success"] = "Course Successfully Added!";
                    return RedirectToPage(nameof(Index));
                }
            }
            return Page();
        }
    }
}
