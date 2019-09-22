using System.Collections.Generic;

namespace My.Custom.ImageField.Models
{
    public class ImageAssetViewModel
    {
        public List<ImageAsset> Images { get; set; }

        public ImageAssetViewModel()
        {
            Images = new List<ImageAsset>();
        }
    }
}