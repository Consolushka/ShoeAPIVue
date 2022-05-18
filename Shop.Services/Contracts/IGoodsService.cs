using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;

namespace Shop.Services.Contracts
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