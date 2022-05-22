using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Basis;

namespace Shop.Repositories.Contracts
{
    public interface IBasketRepository: IAddable<Basket>
    {
        Task<Basket> GetByUser(long id);
    }
}