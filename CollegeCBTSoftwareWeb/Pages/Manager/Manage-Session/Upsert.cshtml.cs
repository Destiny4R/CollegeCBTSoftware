using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeCBTSoftwareWeb.Pages.Manager.Manage_Session
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        [BindProperty]
        public SessionTable session { get; set; }
        public UpsertModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(int? id)
        {
            if (id > 0)
            {
                var ses = dbContext.SessionTables.Find(id);
                if (ses != null)
                {
                    session = ses;
                    return Page();
                }
            }
            session = new SessionTable();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (session.Name != null)
            {
                if (session.Id > 0)
                {
                    var classNSub = dbContext.SessionTables.Find(session.Id);
                    if (classNSub != null)
                    {
                        classNSub.Name = session.Name;
                        dbContext.Update(classNSub);
                        dbContext.SaveChanges();
                        TempData["success"] = "Session/Year Successfully Updated!";
                        return RedirectToPage("Index");
                    }
                }
                SessionTable sessionYear = new()
                {
                    Name = session.Name
                };
                dbContext.SessionTables.Add(sessionYear);
                int result = 0;
                try
                {
                    result = dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Session/Year Already Added!";
                }
                if (result > 0)
                {
                    TempData["success"] = "Session/Year Successfully Added!";
                    return RedirectToPage(nameof(Index));
                }
            }
            return Page();
        }
    }
}
