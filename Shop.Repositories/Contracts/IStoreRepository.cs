using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IStoreRepository: IBaseRepository<Store>
    {
        Task<List<Store>> GetByAvailableGood(long goodId);
    }
}