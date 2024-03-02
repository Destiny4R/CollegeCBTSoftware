using CollegeCBTSoftwareAccess;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.PortableExecutable;

namespace CollegeCBTSoftwareWeb.Pages.Student_Manager
{
    public class RegisterViaFileModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        IExcelDataReader reader;
        public RegisterViaFileModel(ApplicationDbContext dbContext,
            IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }
        public class Message
        {
            public int Success { get; set; }
            public int Failed { get; set; }
            public int Exist { get; set; }
        }
        public int some = 0;
        public Message Messages { get; set; }
        public SelectOptionsVM SelectOptions { get; set; }
        [BindProperty]
        public selectionProperty selection { get; set; }
        public class selectionProperty
        {
            [Required]
            [Display(Name = "Upload Excel File")]
            public IFormFile ExcelFile { get; set; }
            [Required]
            [Display(Name ="Session/Year")]
            public int SessionId { get; set; }
            [Required]
            public string Semester { get; set; }
            [Required]
            public string Level { get; set; }
            [Required]
            public int Course { get; set; }
        }
        public void OnGet()
        {
            Getselections();

        }
        public async Task<IActionResult> OnPost()
        {
            Getselections();
            if (ModelState.IsValid)
            {
                if (selection.ExcelFile == null)
                {
                    TempData["error"] = "File is Not Received...";
                    return Page();
                }

                // Create the Directory if it is not exist
                string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedReports");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // MAke sure that only Excel file is used 
                string dataFileName = Path.GetFileName(selection.ExcelFile.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                {
                    TempData["error"] = "Sorry! This file is not allowed, make sure the file is having any of these extension: .xls or .xlsx";
                    return Page();
                }
                // Make a Copy of the Posted File from the Received HTTP Request
                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    selection.ExcelFile.CopyTo(stream);
                }
                int ree = 0, ess = 0;
                // Use this to handle Encodeing differences in .NET Core
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new FileStream(saveToPath, FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();
                    int success = 0, failed = 0, exist = 0;
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable serviceDetails = ds.Tables[0];
                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            string Fullname = serviceDetails.Rows[i][0].ToString();
                            string Gender = serviceDetails.Rows[i][1].ToString().Replace(" ", "");
                            string Regnumber = serviceDetails.Rows[i][2].ToString().Replace(" ", "");
                            if(!string.IsNullOrEmpty(Fullname) && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(Regnumber))
                            {
                                switch (await UserRegister(Fullname, Gender, Regnumber))
                                {
                                    case 1:
                                        success++;
                                        break;
                                    case 2:
                                        exist++;
                                        break;
                                    default:
                                        failed++;
                                        break;
                                }

                            }
                        }
                    }
                    Messages = new Message()
                    {
                        Success = success,
                        Failed = failed,
                        Exist = exist
                    };
                    some = 2;
                    return Page();
                }
            }
            TempData["error"] = "Select all fields";
            return Page();
        }
        async Task<int> UserRegister(string fullname, string gender, string regnumber)
        {
            var checkUser = await userManager.FindByNameAsync(regnumber);
            if (checkUser != null)
            {
                return 2;
            }
            var user = new ApplicationUser()
            {
                UserName = regnumber,
                Email = regnumber
            };
            var result = await userManager.CreateAsync(user, regnumber);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SD.Role_Student);
                var _student = new StudentTable()
                {
                    FullName = fullname,
                    Gender = gender,
                    SessionYearId = selection.SessionId,
                    ApplicationUserId = user.Id
                };
                dbContext.StudentTables.Add(_student);
                var outcome = dbContext.SaveChanges();
                if (outcome > 0)
                {
                    return outcome;
                }
            }
            return 0;
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
