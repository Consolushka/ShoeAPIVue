using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    internal class GoodsService: IGoodsService
    {
        private readonly IGoodsRepository _repository;
        private readonly IMapper _mapper;

        public GoodsService(IGoodsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Good>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Good> GetById(long id)
        {
            var matched = await _repository.GetById(id);
            if (matched == null)
            {
                throw new Exception($"Cannot find Good with id: {id}");
            }
            return await _repository.GetById(id);
        }

        public async Task<Good> Add(GoodVm goodVm)
        {
            var good = _mapper.Map<Good>(goodVm);
            if (good == null)
            {
                return null;
            }
            var matched = await _repository.GetSameGood(good);
            if (matched != null)
            {
                throw new Exception("Same Good already exists");
            }
            return await _repository.Add(good);
        }

        public async Task<Good> Update(GoodVm goodVm, long id)
        {
            var good = await GetById(id);
            if (good == null)
            {
                throw new Exception($"Cannot find Good with id: {id}");
            }
            if (goodVm == null)
            {
                return null;
            }
            good.FillFromVm(goodVm);
            var matched = await _repository.GetSameGood(good);
            if (matched != null)
            {
                throw new Exception("Same Good already exists");
            }
            return await _repository.Update(good);
        }

        public async Task Delete(long id)
        {
            var matched = await _repository.GetById(id);
            if (matched == null)
            {
                throw new Exception($"Cannot find Good with id: {id}");
            }
            await _repository.Delete(id);
        }
    }
}