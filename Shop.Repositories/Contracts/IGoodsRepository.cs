using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IGoodsRepository: IBaseRepository<Good>
    {
        Task<Good> GetSameGood(Good good);
    }
}