using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;

namespace Shop.Services.Contracts
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<Brand> GetById(long id);
        Task<Brand> Add(BrandVM brandVm);
        Task<Brand> Update(BrandVM brandVm, long id);
        Task Delete(long id);
    }
}