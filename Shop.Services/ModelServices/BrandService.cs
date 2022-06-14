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
    internal class BrandService: IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _brandRepository.GetAll();
        }

        public async Task<Brand> GetById(long id)
        {
            var res = await _brandRepository.GetById(id);
            if (res == null)
            {
                throw new Exception($"Cannot find Brand with id: {id}");
            }

            return res;
        }

        public async Task<Brand> Add(BrandVM brandVm)
        {
            var sameBrand = await _brandRepository.GetByName(brandVm.Name);
            if (sameBrand != null)
            {
                throw new Exception($"Same Brand already exists");
            }
            var brand = _mapper.Map<Brand>(brandVm);
            return await _brandRepository.Add(brand);
        }

        public async Task<Brand> Update(BrandVM brandVm, long id)
        {
            var brand = await GetById(id);
            if (brand == null)
            {
                throw new Exception($"Cannot find Brand with id: {id}");
            }
            var sameBrand = await _brandRepository.GetByName(brandVm.Name);
            if (sameBrand != null)
            {
                throw new Exception($"Same Brand already exists");
            }
            brand.Name = brandVm.Name;
            return await _brandRepository.Update(brand);
        }

        public async Task Delete(long id)
        {
            try
            {
                await _brandRepository.Delete(id);
            }
            catch (System.NullReferenceException ex)
            {
                throw new Exception($"Cannot find Brand with id: {id}");
            }
        }
    }
}