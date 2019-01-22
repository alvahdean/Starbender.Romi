namespace Starbender.Romi.WebApi.Areas.Home.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Starbender.Romi.Data.Models;
    using Starbender.Romi.WebApi.Models;

    public class HomeController : Controller
    {
        private readonly SignInManager<RomiUser> _signInManager;

        private readonly UserManager<RomiUser> _userManager;

        public HomeController(SignInManager<RomiUser> signInManager, UserManager<RomiUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}