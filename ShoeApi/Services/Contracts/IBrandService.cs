using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<Brand> GetById(long Id);
        Task<Brand> Add(BrandVM brandVm);
        Task<Brand> Update(BrandVM brandVm, long id);
        void Delete(long id);
    }
}