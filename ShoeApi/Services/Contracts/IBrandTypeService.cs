using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface IBrandTypeService
    {
        Task<List<BrandType>> GetBrandsByType(long id);
        Task<List<BrandType>> GetTypesByBrand(long id);
        Task<BrandType> TryToAdd(BrandType brandType);
    }
}