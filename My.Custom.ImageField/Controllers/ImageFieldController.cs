using My.Custom.ImageField.Infrastructure.Search;
using My.Custom.ImageField.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace My.Custom.ImageField.Controllers
{
    public class ImageFieldController : Controller
    {
        private readonly AssetFactory _factory;
        private readonly AssetSearchService _searchService;

        public ImageFieldController()
        {
            _factory = new AssetFactory();
            _searchService = new AssetSearchService("dam_image_index");
        }
        
        public ActionResult Search()
        {
            var viewModel = new AssetViewModel();

            var result = _searchService.Search(string.Empty, 0, 100);

            viewModel.Images = result.Select(searchResult => new Asset(){Id = Guid.Parse(searchResult.Id), Name = searchResult.Name, ImageUrl = searchResult.Url}).ToList();

            return View("~/Views/Search/Index.cshtml", viewModel);
        }

        public ActionResult DoSearch(string query = "")
        {
            var viewModel = new AssetViewModel();

            var result = _searchService.Search(query, 0, 100);

            var searchResultViewModel = result.Select(searchResult => new Asset() { Id = Guid.Parse(searchResult.Id), Name = searchResult.Name, ImageUrl = searchResult.Url }).ToList();

            return View("~/Views/Search/AjaxSearchResult.cshtml", searchResultViewModel);
        }        
    }
}