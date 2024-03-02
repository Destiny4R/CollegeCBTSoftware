using CollegeCBTSoftwareAccess;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CollegeCBTSoftwareWeb.Pages
{
    [Authorize(Roles =SD.Role_Student)]
    public class StartExaminationModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public IEnumerable<QuestionTable> questions { get; set; }
        public ExamReady exam { get; set; }
        public string FullName { get; set; }
        public string RegNumber { get; set; }

        public List<int> idz = new List<int>();
        public selectionProperty selection { get; set; }
        public class selectionProperty
        {
            public string Term { get; set; }
            public string ClassId { get; set; }
            public string SubjectId { get; set; }
            public string SessionId { get; set; }
        }
        public class simple
        {
            public int ClassId { get; set; }
            public int SubjectId { get; set; }
            public int SessionId { get; set; }
        }
        public simple Simple { get; set; }
        public StartExaminationModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int start)
        {
            var ClaimsId = (ClaimsIdentity)User.Identity;
            var claim = ClaimsId.FindFirst(ClaimTypes.NameIdentifier);
            var studentinfo = dbContext.StudentTables.Include(g => g.ApplicationUser).FirstOrDefault(d => d.ApplicationUserId == claim.Value);
            if (studentinfo != null)
            {
                FullName = studentinfo.FullName;
                RegNumber = studentinfo.ApplicationUser.UserName;
                var verify = dbContext.ExamReadies.Include(m => m.SessionTable).Include(m => m.CourseTable).FirstOrDefault(n => n.Id == start);
                if (verify != null)
                {
                    exam = verify;
                }
                else
                {
                    TempData["error"] = "An error occured identifying correct information for this course";
                    return RedirectToPage("GetStarted");
                }
                var courseReg = dbContext.StudentCourseReg.FirstOrDefault(u => u.LevelExamReg.Level == verify.Level && u.LevelExamReg.Semester == verify.Semester && u.CourseId == verify.CourseId && u.LevelExamReg.SessionYearId == verify.SessionYearId && u.LevelExamReg.StudentId== studentinfo.Id);
                if (courseReg != null)
                {
                    if (courseReg.Taken)
                    {
                        TempData["error"] = "You have already seated for this course, contact your examination officer for more";
                        return RedirectToPage("GetStarted");
                    }
                }

                questions = dbContext.QuestionTables.Where(k => k.CourseId == verify.CourseId && k.Semester==verify.Semester).ToList();

                foreach (var item in questions)
                {
                    idz.Add(item.Id);
                }
            }
            return Page();
        }
    }
}
