using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRegionsRepository
    {
        RegionesProductoDTO getRegionProduct(string id);
        List<RegionesProductoDTO> getAllRegions();
        List<Pais> getAllCountries();
        Task<int> CreateRegion(List<RegionesProducto> regions);
        Task<int> CreateRelatedProducts(List<ProductosRelacionados> products);
        int DeleteRegion(string id, bool save);
    }
}
