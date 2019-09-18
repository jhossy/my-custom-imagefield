using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using My.Custom.ImageField.Models;
using Sitecore.ContentSearch;

namespace My.Custom.ImageField.Infrastructure.Indexing
{
    public class IndexableImage : IIndexable
    {
        private readonly Asset _asset;
        public IndexableImage(Asset asset)
        {
            _asset = asset;
        }

        public void LoadAllFields()
        {
            Fields = _asset.GetType()
                .GetProperties()
                .Where(fi => fi.GetCustomAttribute<IndexInfo>() != null)
                .Select(fi => new IndexableImageField(_asset, fi));
        }

        public IIndexableDataField GetFieldById(object fieldId)
        {
            return Fields.FirstOrDefault(x => x.Id.Equals(fieldId));
        }

        public IIndexableDataField GetFieldByName(string fieldName)
        {
            return Fields.FirstOrDefault(x => x.Name.Equals(fieldName));
        }

        public IIndexableId Id => new IndexableId<string>(Guid.NewGuid().ToString());
        public IIndexableUniqueId UniqueId => new IndexableUniqueId<IIndexableId>(Id);
        public string DataSource => "DamAssets";
        public string AbsolutePath => "/";
        public CultureInfo Culture => new CultureInfo("en");
        public IEnumerable<IIndexableDataField> Fields { get; private set; }
    }
}