using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeCBTSoftwareWeb.Pages.Manager.Manage_Session
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public IEnumerable<SessionTable> SessionYears { get; set; }
        public int data = 0;
        public IndexModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            SessionYears = dbContext.SessionTables.ToList();
            
        }
    }
}
