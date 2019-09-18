using System;
using System.Collections.Generic;

namespace My.Custom.ImageField.Models
{
    public class AssetFactory
    {
        public List<Asset> CreateList()
        {
            List<Asset> result = new List<Asset>();

            for (int i = 0; i < 50; i++)
            {
                result.Add(new Asset()
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