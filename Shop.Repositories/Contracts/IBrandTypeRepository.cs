using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;
using Type = Shop.Data.Models.Type;

namespace Shop.Repositories.Contracts
{
    public interface IBrandTypeRepository: IBaseRepository<BrandType>
    {
        Task<List<Type>> GetByBrand(long brandId);
        Task<List<Brand>> GetByType(long brandId);
    }
}