namespace Starbender.Romi.Web.Service.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Web.Service.Models;

    public class HomeController : Controller
    {
        private SignInManager<RomiUser> _signInManager;

        private UserManager<RomiUser> _userManager;

        public HomeController(SignInManager<RomiUser> signInManager, UserManager<RomiUser> userManager)
        {
            this._signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                if (cookieKey == ".AspNetCore.Identity.Application" || cookieKey.StartsWith(".AspNetCore.Antiforgery."))
                {
                    Response.Cookies.Delete(cookieKey);
                }
            }

            return this.Redirect("/Identity/Account/Logout");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
