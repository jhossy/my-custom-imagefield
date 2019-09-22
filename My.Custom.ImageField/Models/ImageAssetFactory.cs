using System;
using System.Collections.Generic;

namespace My.Custom.ImageField.Models
{
    public class ImageAssetFactory
    {
        public List<ImageAsset> CreateList()
        {
            List<ImageAsset> result = new List<ImageAsset>();

            for (int i = 0; i < 50; i++)
            {
                result.Add(new ImageAsset()
                {
                    Id = Guid.NewGuid(),
                    Name = "image " + i,
                    ImageUrl = "https://picsum.photos/id/" + i + "/200/300"
                });
            }

            return result;
        }
    }
}