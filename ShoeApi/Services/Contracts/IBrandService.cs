using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(long Id);
        Brand Add(Brand brand);
        Task<Brand> Update(Brand brand);
        void Delete(long id);
    }
}