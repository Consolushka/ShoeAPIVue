using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    public class BasketService: IBasketService
    {
        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Basket> GetByUser(long id)
        {
            var res = await _repository.GetByUser(id);
            if (res == null)
            {
                throw new ($"Cannot find User with id: {id}");
            }

            return res;
        }
    }
}