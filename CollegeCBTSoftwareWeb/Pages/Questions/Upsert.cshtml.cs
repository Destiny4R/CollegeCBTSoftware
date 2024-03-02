using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTInformationSystemWeb.Pages.Questions
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public SelectOptionsVM SelectOptions { get; set; }
        [BindProperty]
        public QuestionVM Question { get; set; }
        public UpsertModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int id)
        {
            Getselections();
            if (id > 0)
            {
                Question = dbContext.QuestionTables.Where(j => j.Id == id).Select(q => new QuestionVM
                {
                    Id = q.Id,
                    Question = q.Question,
                    OptionOne = q.OptionOne,
                    OptionTwo = q.OptionTwo,
                    OptionThree = q.OptionThree,
                    OptionFour = q.OptionFour,
                    Semester = q.Semester,
                    Answer = q.Answer,
                    Level = q.Level,
                    Course = q.CourseId
                }).FirstOrDefault();
                if(Question == null)
                {
                    TempData["error"] = "Undentity question!";
                    return RedirectToPage("Index");
                }
                return Page();
            }
            Question = new QuestionVM();
            return Page();
        }
        public IActionResult OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                if (Question.Id > 0)
                {
                    var questionz = dbContext.QuestionTables.FirstOrDefault(j => j.Id == Question.Id);
                    if(questionz!= null)
                    {
                        questionz.Question = Question.Question;
                        questionz.OptionOne = Question.OptionOne;
                        questionz.OptionTwo = Question.OptionTwo;
                        questionz.OptionThree = Question.OptionThree;
                        questionz.OptionFour = Question.OptionFour;
                        questionz.Semester = Question.Semester;
                        questionz.Answer = Question.Answer;
                        questionz.Level = Question.Level;
                        questionz.CourseId = Question.Course;
                        dbContext.Update(questionz);
                        TempData["success"] = "Question Successfully Updated";
                        dbContext.SaveChanges();
                        return RedirectToPage("Index");
                    }
                }
                else
                {
                    var quest = new QuestionTable()
                    {
                        Question = Question.Question,
                        OptionOne = Question.OptionOne,
                        OptionTwo = Question.OptionTwo,
                        OptionThree = Question.OptionThree,
                        OptionFour = Question.OptionFour,
                        Semester = Question.Semester,
                        Answer = Question.Answer,
                        Level = Question.Level,
                        CourseId = Question.Course
                    };
                    dbContext.Add(quest);
                    dbContext.SaveChanges();
                    TempData["success"] = "Question Successfully Submitted";
                    return Page();
                }
            }
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
