using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CollegeCBTSoftwareWeb.Pages.Student_Manager.StudentExamRegister
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public SelectOptionsVM SelectOptions { get; set; }
        [BindProperty]
        public ExamRegVM ExamReg { get; set; }
        public int check = 0;
        public EditModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int id)
        {
            Getselections();
            if (id > 0)
            {
                ExamReg = dbContext.LevelRegistrations.Include(l => l.StudentTable).ThenInclude(i=>i.ApplicationUser).Include(l=>l.StudentCourseReg).ThenInclude(l=>l.CourseTable).Where(k=>k.Id==id).Select(k=> new ExamRegVM
                {
                    Id = k.Id,
                    StudentId = k.StudentId,
                    Semester = k.Semester,
                    Level = k.Level,
                    SessionYearId = k.SessionYearId,
                    StudentCourses = k.StudentCourseReg,
                    Student = k.StudentTable
                }).FirstOrDefault();
                if(ExamReg == null)
                {
                    TempData["error"] = "Unidentify student information!";
                    return RedirectToPage("Index");
                }
                if(ExamReg.StudentCourses.Any())
                {
                    check = 2;
                }
                return Page();
            }
            TempData["error"] = "Unidentify student date!";
            return RedirectToPage("Index");
        }
        public IActionResult OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                var exams = dbContext.LevelRegistrations.FirstOrDefault(k => k.Id == ExamReg.Id);
                if (exams != null)
                {
                    exams.Semester = ExamReg.Semester;
                    exams.SessionYearId = ExamReg.SessionYearId;
                    exams.Level = ExamReg.Level;
                    dbContext.Update(exams);
                    if (ExamReg.Courses.Count() > 0)
                    {
                        var courseCollection = new Collection<StudentCourseReg>();
                        foreach (var item in ExamReg.Courses)
                        {
                            var course = new StudentCourseReg()
                            {
                                CourseId = item,
                                LevelExamId = ExamReg.Id
                            };
                            courseCollection.Add(course);
                        }
                        dbContext.Add(courseCollection);
                    }
                    int result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Student Successfully Updated!";
                        return RedirectToPage("Index");
                    }
                }
                TempData["error"] = "An Error Occured While Updating Student  Information!";
                return Page();
            }
            TempData["error"] = "An Student Information Updating Failed!";
            return Page();
        }
        private void Getselections()
        {
            SelectOptions = new()
            {
                _Sessions = dbContext.SessionTables.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                _Course = dbContext.CourseTables.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
        }
    }
}
