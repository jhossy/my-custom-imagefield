using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Runtime.Serialization;

namespace My.Custom.ImageField.Infrastructure.Search
{
    public class ImageSearchResultItem : SearchResultItem
    {
        [DataMember]
        [IndexField("assetid")]
        public string AssetId { get; set; }

        [DataMember]
        [IndexField("imageurl")]
        public string ImageUrl { get; set; }

        [DataMember]
        [IndexField("assetname")]
        public string AssetName { get; set; }

    }
}