using System.Collections.Generic;

namespace My.Custom.ImageField.Models
{
    public class AssetViewModel
    {
        public List<Asset> Images { get; set; }

        public AssetViewModel()
        {
            Images = new List<Asset>();
        }
    }
}