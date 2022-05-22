using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Contracts
{
    public interface IStoreService
    {
        Task<List<Store>> GetAll();
        Task<Store> GetById(long id);
        Task<List<Store>> GetByAvailableGood(long id);
        Task<Store> Add(Store store);
        Task Update(Store store);
        Task Delete(long id);
    }
}