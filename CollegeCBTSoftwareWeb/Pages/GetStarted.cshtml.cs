using CollegeCBTSoftwareAccess;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CollegeCBTSoftwareWeb.Pages
{
    [Authorize(Roles =SD.Role_Student)]
    public class GetStartedModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        [BindProperty]
        public selectionProperty selection {  get; set; }
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
        [ValidateNever]
        public string RegNumber { get; set; }
        [ValidateNever]
        public string FullName { get; set; }
        public SelectOptionsVM SelectOptions { get; set; }
        public GetStartedModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Getselections();
            var ClaimsId = (ClaimsIdentity)User.Identity;
            var claim = ClaimsId.FindFirst(ClaimTypes.NameIdentifier);
            var studentinfo = dbContext.StudentTables.Include(g => g.ApplicationUser).FirstOrDefault(d => d.ApplicationUserId == claim.Value);
            if (studentinfo != null)
            {
                FullName = studentinfo.FullName;
                RegNumber = studentinfo.ApplicationUser.UserName;
            }
        }
        public IActionResult OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                var verify = dbContext.ExamReadies.FirstOrDefault(k => k.Semester == selection.Semester && k.Level == selection.Level && k.SessionYearId == selection.SessionId && k.CourseId == selection.Course);
                if (verify != null)
                {
                    if (verify.PublishExam)
                    {
                        return RedirectToPage("StartExamination", new { start = verify.Id });
                    }
                    TempData["error"] = "You are not allowed to write this paper at this time, check again later";
                    return Page();
                }
                TempData["error"] = "No data found for the selected information";
                return Page();
            }
            TempData["error"] = "Select all required fields";
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
