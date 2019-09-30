using My.Custom.ImageField.Infrastructure.Search;
using My.Custom.ImageField.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace My.Custom.ImageField.Controllers
{
    public class ImageFieldController : Controller
    {
        private readonly ImageAssetFactory _factory;
        private readonly ImageSearchService _searchService;

        private readonly int _startPage = 0;
        private readonly int _pageSize = 100;

        public ImageFieldController()
        {
            _factory = new ImageAssetFactory();
            _searchService = new ImageSearchService("my_image_index");
        }
        
        public ActionResult Search()
        {
            var viewModel = new ImageAssetViewModel();

            viewModel.Images = ExecuteSearch(string.Empty, _startPage, _pageSize);

            return View("~/Views/Search/Index.cshtml", viewModel);
        }

        public ActionResult DoSearch(string query = "")
        {
            List<ImageAsset> searchResult = ExecuteSearch(query, _startPage, _pageSize);

            return View("~/Views/Search/AjaxSearchResult.cshtml", searchResult);
        }        

        private List<ImageAsset> ExecuteSearch(string query, int currentPage, int pageSize)
        {
            var result = _searchService.Search(query, currentPage, pageSize);

            return result
                .Select(searchResult => new ImageAsset() { Id = Guid.Parse(searchResult.Id), Name = searchResult.Name, ImageUrl = searchResult.Url })
                .ToList();
        }
    }
}