using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IGoodsService
    {
        Task<List<Good>> GetAll();
        Task<Good> GetById(long id);
        Task<Good> Add(GoodVm good);
        Task<Good> Update(GoodVm good, long id);
        Task Delete(long id);
    }
}