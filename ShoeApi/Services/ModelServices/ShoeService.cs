using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
{
    public class ShoeService: IShoeService
    {
        private readonly IShoeRepository _repository;

        public ShoeService(IShoeRepository repository)
        {
            _repository = repository;
        }

        public List<Shoe> GetAll()
        {
            return _repository.GetAll();
        }

        public Shoe GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Shoe Add(Shoe shoe)
        {
            return _repository.Add(shoe).Result;
        }

        public async Task<Shoe> Update(Shoe shoe)
        {
            return await _repository.Update(shoe);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}