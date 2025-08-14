using Domain.Interfaces;
using Models.Utilities;
using System.Collections.Generic;

namespace Talos.API.Classes
{
    public class ProductsHelper
    {
        private readonly IDriveRepository driveRepository;
        public ProductsHelper(IDriveRepository driveRepository)
        {
            this.driveRepository = driveRepository;
        }

        public List<KeyValuePair<string, string>> VerifyProductImages(List<KeyValuePair<string, string>> images)
        {
            foreach (var image in images)
            {
                if (!Helper.checkBase64String(image.Value))
                {
                    images.Remove(image);
                    var newImage = driveRepository.GetFileById(image.Key);

                    if(Helper.checkBase64String(newImage))
                        images.Add(new KeyValuePair<string,string>(image.Key, newImage));
                }
            }
            
            return images;
        }
    }
}
