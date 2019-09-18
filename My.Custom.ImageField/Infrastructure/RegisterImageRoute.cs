using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace My.Custom.ImageField.Infrastructure
{
    public class RegisterImageRoute
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("ImageSearch", "api/imagefield/search", new { controller = "ImageField", action = "Search" });
            RouteTable.Routes.MapRoute("ImageDoSearch", "api/imagefield/dosearch/{query}", new { controller = "ImageField", action = "doSearch", query = UrlParameter.Optional });
        }
    }
}