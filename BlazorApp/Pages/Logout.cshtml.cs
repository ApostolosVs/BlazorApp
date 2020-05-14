using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class LogoutModel : PageModel
    {
        // If the user press the log out component then 
        public async Task OnGet()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync("cookie");
                await HttpContext.SignOutAsync("oidc");
                Response.Redirect("/");
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}