using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeCBTSoftwareWeb.Pages.PublishExams
{
    public class ResultSheetModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public SelectOptionsVM SelectOptions { get; set; }
        [BindProperty]
        public selectionProperty selection { get; set; }
        public IEnumerable<StudentCourseReg> CourseReg { get; set; }
        public class selectionProperty
        {
            [Required]
            public string Semester { get; set; }
            [Required]
            public string Level { get; set; }
            [Required]
            public int Course { get; set; }
            [Required]
            public int SessionId { get; set; }
        }
        public int check = 0;
        public ResultSheetModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Getselections();
        }
        public IActionResult OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                var courseReg = dbContext.StudentCourseReg.Include(f=>f.LevelExamReg.StudentTable.ApplicationUser).Include(f=>f.CourseTable).Include(f=>f.LevelExamReg.SessionTable).Where(u=>u.LevelExamReg.Level== selection.Level && u.LevelExamReg.Semester== selection.Semester && u.CourseId== selection.Course && u.LevelExamReg.SessionYearId== selection.SessionId).ToList();
                if (courseReg.Count() < 1)
                {
                    TempData["error"] = "Result Not found for the selected information, check and try again!";
                    return Page();
                }
                CourseReg = courseReg;
                check = 2;
                TempData["success"] = " Result found for your selection!";
                return Page();
            }
            TempData["error"] = "Select all appropriate areas before searching!";
            return Page();
        }
        private void Getselections()
        {
            SelectOptions = new()
            {
                _Course = dbContext.CourseTables.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                _Sessions = dbContext.SessionTables.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
        }
    }
}
