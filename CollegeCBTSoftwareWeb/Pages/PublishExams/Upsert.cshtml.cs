using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeCBTSoftwareWeb.Pages.PublishExams
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public SelectOptionsVM SelectOptions { get; set; }
        [BindProperty]
        public ExamsReadyVM pubish { get; set; }
        public UpsertModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int id)
        {
            Getselections();
            if (id > 0)
            {
                pubish = dbContext.ExamReadies.Where(j => j.Id == id).Select(q => new ExamsReadyVM
                {
                    Id = q.Id,
                    Semester = q.Semester,
                    SessionYearId = q.SessionYearId,
                    Level = q.Level,
                    CourseId = q.CourseId,
                    StartDate = q.StartDate,
                    Duration = q.Duration,
                    NoToAnswer = q.NoToAnswer
                }).FirstOrDefault();
                if (pubish == null)
                {
                    TempData["error"] = "Undentity Publication Data!";
                    return RedirectToPage("Index");
                }
                return Page();
            }
            pubish = new ExamsReadyVM();
            return Page();
        }
        public IActionResult OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                if (pubish.Id > 0)
                {
                    var publishData = dbContext.ExamReadies.FirstOrDefault(j => j.Id == pubish.Id);
                    if (publishData != null)
                    {
                        publishData.Semester = pubish.Semester;
                        publishData.SessionYearId = pubish.SessionYearId;
                        publishData.Level = pubish.Level;
                        publishData.CourseId = pubish.CourseId;
                        publishData.StartDate = pubish.StartDate;
                        publishData.Duration = pubish.Duration;
                        publishData.NoToAnswer = pubish.NoToAnswer;
                        dbContext.Update(publishData);
                        TempData["success"] = "Question Successfully Updated";
                        dbContext.SaveChanges();
                        return RedirectToPage("Index");
                    }
                }
                else
                {
                    var quest = new ExamReady()
                    {
                        Semester = pubish.Semester,
                        SessionYearId = pubish.SessionYearId,
                        Level = pubish.Level,
                        CourseId = pubish.CourseId,
                        StartDate = pubish.StartDate,
                        Duration = pubish.Duration,
                        NoToAnswer = pubish.NoToAnswer
                };
                    dbContext.Add(quest);
                    dbContext.SaveChanges();
                    TempData["success"] = "Examination successfully published for the selected Class";
                    return RedirectToPage("Index");
                }
            }
            TempData["error"] = "An error occured while publishing the class examination";
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
