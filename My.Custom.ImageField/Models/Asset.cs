using My.Custom.ImageField.Infrastructure.Indexing;
using System;

namespace My.Custom.ImageField.Models
{
    public class Asset
    {
        [IndexInfo("assetid")]
        public Guid Id { get; set; }

        [IndexInfo("imageurl")]
        public string ImageUrl { get; set; }

        [IndexInfo("assetname")]
        public string Name { get; set; }
    }
}