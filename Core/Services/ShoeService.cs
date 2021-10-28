using System.Collections.Generic;
using Entities.Models;
using Repository.Contracts;
using Core.Contracts;

namespace Core.Services
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

        public long Add(Shoe shoe)
        {
            return _repository.Add(shoe).Result;
        }

        public Shoe Update(Shoe shoe)
        {
            return _repository.Update(shoe);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}