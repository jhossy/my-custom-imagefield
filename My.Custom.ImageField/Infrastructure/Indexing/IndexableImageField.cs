using System;
using System.Reflection;
using Sitecore.ContentSearch;

namespace My.Custom.ImageField.Infrastructure.Indexing
{
    public class IndexableImageField : IIndexableDataField
    {
        private readonly Models.ImageAsset _asset;
        private readonly PropertyInfo _fieldInfo;

        public IndexableImageField(Models.ImageAsset concreteObject, PropertyInfo fieldInfo)
        {
            _asset = concreteObject;
            _fieldInfo = fieldInfo;
        }

        public string Name
        {
            get
            {
                var info = _fieldInfo.GetCustomAttribute<IndexInfo>();
                return info.Name;
            }
        }

        public string TypeKey => string.Empty;
        public Type FieldType => _fieldInfo.PropertyType;
        public object Value => _fieldInfo.GetValue(_asset);
        public object Id => _fieldInfo.Name.ToLower();
    }
}