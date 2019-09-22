using System;
using System.Collections.Generic;
using System.Linq;
using My.Custom.ImageField.Models;
using Sitecore.ContentSearch;

namespace My.Custom.ImageField.Infrastructure.Indexing
{
    public class CustomImageCrawler : FlatDataCrawler<IndexableImage>
    {
        private readonly ImageAssetFactory _factory = new ImageAssetFactory();
        private List<ImageAsset> _allAssets = null;
        private List<ImageAsset> AllAssets
        {
            get
            {
                if (_allAssets == null)
                {
                    _allAssets = new List<ImageAsset>();
                    _allAssets = _factory.CreateList();
                }

                return _allAssets;
            }
        }

        protected override IndexableImage GetIndexableAndCheckDeletes(IIndexableUniqueId indexableUniqueId)
        {
            var asset = AllAssets.FirstOrDefault(x => x.Id == (Guid)indexableUniqueId.Value);
            if (asset == null) return null;
            return new IndexableImage(asset);
        }

        protected override IndexableImage GetIndexable(IIndexableUniqueId indexableUniqueId)
        {
            var asset = AllAssets.FirstOrDefault(x => x.Id == (Guid)indexableUniqueId.Value);
            if (asset == null) return null;
            return new IndexableImage(asset);
        }

        protected override bool IndexUpdateNeedDelete(IndexableImage indexable)
        {
            return false;
        }

        protected override IEnumerable<IIndexableUniqueId> GetIndexablesToUpdateOnDelete(IIndexableUniqueId indexableUniqueId)
        {
            var asset = AllAssets.FirstOrDefault(x => x.Id == (Guid)indexableUniqueId.Value);
            if (asset == null) return null;
            return new List<IIndexableUniqueId>() { indexableUniqueId };
        }

        protected override IEnumerable<IndexableImage> GetItemsToIndex()
        {
            var list = new List<IndexableImage>();
            foreach (Models.ImageAsset asset in AllAssets)
            {
                list.Add(new IndexableImage(asset));
            }
            return list;
        }
    }
}