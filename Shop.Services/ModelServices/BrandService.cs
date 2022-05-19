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

        public async Task<Brand> GetById(long Id)
        {
            return await _brandRepository.GetById(Id);
        }

        public async Task<Brand> Add(BrandVM brandVm)
        {
            var brand = _mapper.Map<Brand>(brandVm);
            if (brand == null)
            {
                return null;
            }
            return await _brandRepository.Add(brand);
        }

        public async Task<Brand> Update(BrandVM brandVm, long id)
        {
            var brand = await GetById(id);
            brand.Name = brandVm.Name;
            return await _brandRepository.Update(brand);
        }

        public async Task Delete(long id)
        {
            await _brandRepository.Delete(id);
        }
    }
}