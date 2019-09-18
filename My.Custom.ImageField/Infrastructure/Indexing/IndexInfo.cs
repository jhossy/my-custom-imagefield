using System;

namespace My.Custom.ImageField.Infrastructure.Indexing
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexInfo : Attribute
    {
        public string Name { get; private set; }

        public IndexInfo(string name)
        {
            Name = name;
        }
    }
}