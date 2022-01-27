using System;
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

        public async Task<List<Shoe>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Shoe> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Shoe> Add(ShoeVM shoeVm)
        {
            var shoe = _mapper.Map<Shoe>(shoeVm);
            if (shoe == null)
            {
                return null;
            }
            return await _repository.Add(shoe);
        }

        public async Task<Shoe> Update(ShoeVM shoeVm, long id)
        {
            if (shoeVm == null)
            {
                return null;
            }
            var shoe = await GetById(id);
            shoe.CompareWithVM(shoeVm);
            return await _repository.Update(shoe);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }
    }
}