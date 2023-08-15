using Domain.Interfaces;
using Models.DTOs;
using System.Collections.Generic;
using Models.Utilities;

namespace Talos.API.Classes
{
    public class CategoriesHelper
    {
        private readonly IDriveRepository _driveRepository;
        public CategoriesHelper(IDriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

        public CategoriaDTO GetImage(CategoriaDTO category)
        {
            if (!Helper.checkBase64String(category.Imagen))
                category.Imagen = _driveRepository.GetFileById(category.Imagen);

            return category;
        }

        public List<CategoriaDTO> GetImageFromList(List<CategoriaDTO> categories)
        {
            foreach (var item in categories)
            {
                if (!Helper.checkBase64String(item.Imagen))
                    item.Imagen = _driveRepository.GetFileById(item.Imagen);
            }
            
            return categories;
        }
    }
}
