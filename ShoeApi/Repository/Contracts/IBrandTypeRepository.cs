using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Repository.Contracts
{
    public interface IBrandTypeRepository: IBaseRepository<BrandType>
    {
        Task<List<Type>> GetByBrand(long brandId);
        Task<List<Brand>> GetByType(long brandId);
    }
}