using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CBTInformationSystemWeb.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public Input input { get; set; }
        public class Input
        {
            [DataType(DataType.Password)]
            [Display(Name = "old password")]
            public required string Oldpassword { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "new password")]
            public required string Newpassword { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "confirm new password")]
            [Compare(nameof(Newpassword), ErrorMessage = "Confirm Password Do not Mathch!")]
            public required string Confirmpassword { get; set; }
        }
        public ChangePasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("Login");
                }
                var result = await userManager.ChangePasswordAsync(user, input.Oldpassword, input.Newpassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                await signInManager.RefreshSignInAsync(user);

                TempData["success"] = "Your Password has been successfully changed!!";
                return Page();
            }
            TempData["error"] = "An error occured while changing the password try again!";
            return Page();
        }
    }
}
