using My.Custom.ImageField.Infrastructure.Search;
using My.Custom.ImageField.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace My.Custom.ImageField.Controllers
{
    public class ImageFieldController : Controller
    {
        private readonly ImageAssetFactory _factory;
        private readonly ImageSearchService _searchService;

        public ImageFieldController()
        {
            _factory = new ImageAssetFactory();
            _searchService = new ImageSearchService("dam_image_index");
        }
        
        public ActionResult Search()
        {
            var viewModel = new ImageAssetViewModel();

            var result = _searchService.Search(string.Empty, 0, 100);

            viewModel.Images = result.Select(searchResult => new ImageAsset(){Id = Guid.Parse(searchResult.Id), Name = searchResult.Name, ImageUrl = searchResult.Url}).ToList();

            return View("~/Views/Search/Index.cshtml", viewModel);
        }

        public ActionResult DoSearch(string query = "")
        {
            var viewModel = new ImageAssetViewModel();

            var result = _searchService.Search(query, 0, 100);

            var searchResultViewModel = result.Select(searchResult => new ImageAsset() { Id = Guid.Parse(searchResult.Id), Name = searchResult.Name, ImageUrl = searchResult.Url }).ToList();

            return View("~/Views/Search/AjaxSearchResult.cshtml", searchResultViewModel);
        }        
    }
}