using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class LoginModel : PageModel
    {   //This function used when authenticated on the Identity server 4
        public async Task OnGet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated) //If the user authenticated succesfully then redirect to customers page!!
            {
                await HttpContext.ChallengeAsync(new AuthenticationProperties { RedirectUri = "/customers" });
            }
            else
            {//if not then redirect to the default page
                Response.Redirect("/");
            }
        }
    }
}