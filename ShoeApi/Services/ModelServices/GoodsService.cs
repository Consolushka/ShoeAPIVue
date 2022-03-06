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
    public class GoodsService: IGoodsService
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
            return await _repository.GetById(id);
        }

        public async Task<Good> Add(GoodVm goodVm)
        {
            var good = _mapper.Map<Good>(goodVm);
            if (good == null)
            {
                return null;
            }
            return await _repository.Add(good);
        }

        public async Task<Good> Update(GoodVm goodVm, long id)
        {
            if (goodVm == null)
            {
                return null;
            }
            var good = await GetById(id);
            good.FillFromVm(goodVm);
            return await _repository.Update(good);
        }

        public async Task Delete(long id)
        {
            var good = await GetById(id);
            await _repository.Delete(good);
        }
    }
}