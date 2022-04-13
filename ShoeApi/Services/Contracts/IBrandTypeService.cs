using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface IBrandTypeService
    {
        Task<IEnumerable<BrandType>> GetAll();
        Task<BrandType> Add(BrandType entity);
        Task Delete(long id);
        Task<List<Brand>> GetBrandsByType(long id);
        Task<List<Type>> GetTypesByBrand(long id);
        Task<BrandType> TryToAdd(BrandType brandType);
    }
}