namespace Starbender.Romi.Web.Service.Views.Home
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class BlogModel : PageModel
    {
        public string Title { get; set; } = "ROMI - Blog";

        public void OnGet()
        {
        }
    }
}