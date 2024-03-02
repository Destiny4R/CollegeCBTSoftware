using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace CollegeCBTSoftwareWeb.Pages.Student_Manager.StudentExamRegister
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        [BindProperty]
        public ExamRegVM ExamReg { get; set; }
        [BindProperty]
        public StudentTable studentTable { get; set; }
        public SelectOptionsVM  SelectOptions { get; set; }
        public RegisterModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Getselections();
            studentTable = new StudentTable();
        }
        public IActionResult OnPostSearch(string RegNumber)
        {
            Getselections();
            studentTable = new StudentTable();
            if (!string.IsNullOrEmpty(RegNumber))
            {
                var student = dbContext.StudentTables.Include(k => k.ApplicationUser).FirstOrDefault(l => l.ApplicationUser.UserName == RegNumber);
                if (student != null)
                {
                    studentTable = student;
                    TempData["success"] = "Student Found, Proceed to examination registration";
                    return Page();
                }
                TempData["error"] = "Student with the provided registration number found!";
                return Page();
            }
            TempData["error"] = "Provide Student correct registration number!";
            return Page();
        }
        public IActionResult OnPostRegist()
        {
            Getselections();
            if (ExamReg != null)
            {
                var ifisregistered = dbContext.LevelRegistrations.FirstOrDefault(p => p.StudentId == studentTable.Id && p.Semester==ExamReg.Semester && p.Level == ExamReg.Level && p.SessionYearId == ExamReg.SessionYearId);
                if (ifisregistered != null)
                {
                    TempData["error"] = "This student is already registered for the term!";
                    return Page();
                }
                var levelreg = new LevelRegistration()
                {
                    StudentId = studentTable.Id,
                    Semester = ExamReg.Semester,
                    Level = ExamReg.Level,
                    SessionYearId = ExamReg.SessionYearId
                };
                if (ExamReg.Courses.Count() > 0)
                {
                    levelreg.StudentCourseReg = new Collection<StudentCourseReg>();
                    foreach (var item in ExamReg.Courses)
                    {
                        var course = new StudentCourseReg()
                        {
                            CourseId = item,
                            LevelExamId = levelreg.Id
                        };
                        levelreg.StudentCourseReg.Add(course);
                    }
                }
                dbContext.Add(levelreg);
                int result = dbContext.SaveChanges();
                if(result > 0)
                {
                    TempData["success"] = "Student Successfully Registered!";
                    return RedirectToPage("Index");
                }
                TempData["error"] = "An Error Occured While Registering The Student!";
                return Page();
            }
            TempData["error"] = "Student Registeration Failed!";
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
