using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareAccess;
using CollegeCBTSoftwareModel.ViewModel;

namespace CollegeCBTSoftwareWeb.Pages.Student_Manager.Registration
{
    public class UpsertModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public StudentVM student {  get; set; }
        public SelectOptionsVM SelectOptions { get; set; }
        public UpsertModel(UserManager<ApplicationUser> _userManager, ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager)
        {
            userManager = _userManager;
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }
        public IActionResult OnGet(int id)
        {
            Getselections();
            if (id > 0)
            {
                var tab = dbContext.StudentTables.Select(k=> new StudentVM{
                    Id = k.Id,
                    FullName = k.FullName,
                    Gender = k.Gender,
                    SessionYearId = k.SessionYearId
                }).FirstOrDefault(k => k.Id == id);
                if (tab != null)
                {
                    student = tab;
                    return Page();
                }
            }
            student = new StudentVM();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                if (student.Id>0)
                {
                    var stud = dbContext.StudentTables.FirstOrDefault(l => l.Id == student.Id);
                    if (stud != null)
                    {
                        stud.FullName = student.FullName;
                        stud.Gender = student.Gender;
                        stud.SessionYearId = student.SessionYearId;
                        dbContext.SaveChanges();
                        TempData["success"] = "Student Information Successfully Updated!";
                        return RedirectToPage("Index");
                    }
                }
                else
                {
                    var stud = dbContext.StudentTables.ToList().Count();
                    if (stud < 1)
                    {
                        stud = 1;
                    }
                    string username = "CBT" + (100 + (stud+1)) + "".ToString();
                    var user = new ApplicationUser()
                    {
                        UserName = username,
                        Email = username
                    };
                    var result = await userManager.CreateAsync(user, student.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, SD.Role_Student);
                        var _student = new StudentTable()
                        {
                            FullName = student.FullName,
                            Gender = student.Gender,
                            SessionYearId = student.SessionYearId,
                            ApplicationUserId = user.Id
                        };
                        dbContext.StudentTables.Add(_student);
                        var outcome = dbContext.SaveChanges();
                        if (outcome > 0)
                        {
                            TempData["success"] = "Student Account and Registration Successful!";
                            return RedirectToPage("Index");
                        }
                    }
                    TempData["error"] = "Account Registration faired!";
                }
            }
            TempData["error"] = "Account Registration faired, fill all areas!";
            return Page();
        }
        private void Getselections()
        {
            SelectOptions = new()
            {
                _Sessions = dbContext.SessionTables.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
            };
        }
    }
}
