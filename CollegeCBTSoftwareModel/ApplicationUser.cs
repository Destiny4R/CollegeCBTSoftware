using Microsoft.AspNetCore.Identity;

namespace CollegeCBTSoftwareModel
{
    public class ApplicationUser:IdentityUser
    {
        public bool Status { get; set; } = true;
    }
}