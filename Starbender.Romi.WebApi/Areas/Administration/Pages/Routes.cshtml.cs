namespace Starbender.Romi.WebApi.Areas.Administration.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Newtonsoft.Json;

    public class RoutesListModel : PageModel
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RoutesListModel(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public string Author { get; set; } = "Dean Fuqua";

        public List<RouteInfo> Routes { get; set; }

        public string Title { get; set; } = "Web Service Routes";

        public void OnGet()
        {
            this.Routes = this._actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(
                x => new RouteInfo
                         {
                             Action = x.RouteValues["Action"],
                             Controller = x.RouteValues["Controller"],
                             Name = x.AttributeRouteInfo?.Name,
                             Template = x.AttributeRouteInfo?.Template,
                             Constraint = x.ActionConstraints == null
                                              ? string.Empty
                                              : JsonConvert.SerializeObject(x.ActionConstraints)
                         }).OrderBy(r => r.Template).ToList();
        }

        public class RouteInfo
        {
            public string Action { get; set; }

            public string Constraint { get; set; }

            public string Controller { get; set; }

            public string Name { get; set; }

            public string Template { get; set; }
        }
    }
}