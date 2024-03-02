using CollegeCBTSoftwareAccess;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CBTInformationSystemWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginModel(ApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [BindProperty]
        public Input Inputs { get; set; }
        public class Input
        {
            public required string username { get; set; }
            public required string password { get; set; }
            public bool rememberme { get; set; }
        }
        public void OnGet()
        {
            var getdata = dbContext.ApplicationUser.ToList();
            if (getdata.Count() < 1)
            {
                AddAdmin();
            }
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user2 = await dbContext.ApplicationUser.FirstOrDefaultAsync(g=>g.UserName==Inputs.username || g.Email == Inputs.username);
                if (user2 != null)
                {
                    if (!user2.Status)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account has been barnd from using this system please contact System Administreator!");
                        return Page();
                    }

                    var result = await signInManager.PasswordSignInAsync(user2, Inputs.password, Inputs.rememberme, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        else { return RedirectToPage("/Index"); }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return Page();
            }
            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return Page();
        }
        private void Addrole()
        {
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Student)).GetAwaiter().GetResult();
            }
        }
        private void AddAdmin()
        {
            Addrole();
            string Admin = "CBTADMIN01";

            var user = new ApplicationUser();

            user.UserName = Admin;
            user.Email = Admin;
            user.PhoneNumber = "00000000";
            string password = "123";
            user.Status = true;

            var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

        }
    }
}
