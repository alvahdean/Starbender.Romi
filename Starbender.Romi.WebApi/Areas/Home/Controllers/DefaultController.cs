using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Starbender.Romi.WebApi.Areas.Home.Controllers
{
    using Microsoft.AspNetCore.Mvc.RazorPages.Internal;

    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}