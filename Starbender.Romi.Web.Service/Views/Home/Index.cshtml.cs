namespace Starbender.Romi.Web.Service.Views.Home
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        public string Title { get; set; } = "ROMI - Home";

        public void OnGet()
        {
        }
    }
}