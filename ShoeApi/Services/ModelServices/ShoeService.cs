using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
{
    public class ShoeService: IShoeService
    {
        private readonly IShoeRepository _repository;
        private readonly IMapper _mapper;

        public ShoeService(IShoeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Shoe> GetAll()
        {
            return _repository.GetAll();
        }

        public Shoe GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Shoe Add(ShoeVM shoeVm)
        {
            var shoe = _mapper.Map<Shoe>(shoeVm);
            return _repository.Add(shoe).Result;
        }

        public async Task<Shoe> Update(ShoeVM shoeVm, long id)
        {
            var shoe = _mapper.Map<Shoe>(shoeVm);
            shoe.Id = id;
            return await _repository.Update(shoe);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}