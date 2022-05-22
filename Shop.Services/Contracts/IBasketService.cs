using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Contracts
{
    public interface IBasketService
    {
        Task<Basket> GetByUser(long id);
    }
}