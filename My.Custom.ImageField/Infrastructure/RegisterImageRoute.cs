using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace My.Custom.ImageField.Infrastructure
{
    public class RegisterImageRoute
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("ImageSearch", "custom/assets/search", new { controller = "Assets", action = "Search" });
            RouteTable.Routes.MapRoute("ImageDoSearch", "custom/assets/dosearch/{query}", new { controller = "Assets", action = "doSearch", query = UrlParameter.Optional });
        }
    }
}