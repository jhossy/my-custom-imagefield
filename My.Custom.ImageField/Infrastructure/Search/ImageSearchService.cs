using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;

namespace My.Custom.ImageField.Infrastructure.Search
{
    public class ImageSearchService
    {
        private readonly string _indexName;

        public ImageSearchService(string indexName)
        {
            _indexName = indexName;
        }

        public List<SearchResult> Search(string query, int start, int size)
        {
            var index = ContentSearchManager.GetIndex(_indexName);
            
            using (var context = index.CreateSearchContext())
            {
                var preResults = context.GetQueryable<ImageSearchResultItem>()
                    .Where(x => x.AssetName.Contains(query))
                    .ToList();

                var results = preResults.Select(x => new SearchResult
                {
                    Name = x.AssetName,
                    Url = x.ImageUrl,
                    Id = x.AssetId
                });
                return results
                    .Skip(start)
                    .Take(size)
                    .ToList();
            }
        }
    }
}