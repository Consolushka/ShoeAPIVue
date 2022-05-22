using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    public class StoreService: IStoreService
    {
        private readonly IStoreRepository _repository;

        public StoreService(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Store>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Store> GetById(long id)
        {
            var res = await _repository.GetById(id);
            if (res == null)
            {
                throw new System.NullReferenceException($"Cannot find Store with id: {id}");
            }

            return res;
        }

        public async Task<List<Store>> GetByAvailableGood(long id)
        {
            return await _repository.GetByAvailableGood(id);
        }

        public async Task<Store> Add(Store store)
        {
            return await _repository.Add(store);
        }

        public async Task Update(Store store)
        {
            try
            {
                await _repository.Update(store);
            }
            catch(System.NullReferenceException ex)
            {
                throw new System.NullReferenceException($"Cannot find Store with id: {store.Id}");
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch(System.NullReferenceException ex)
            {
                throw new System.NullReferenceException($"Cannot find Store with id: {id}");
            }
        }
    }
}