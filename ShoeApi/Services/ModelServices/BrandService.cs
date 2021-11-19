using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
{
    public class BrandService: IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public List<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public Brand GetById(long Id)
        {
            return _brandRepository.GetById(Id);
        }

        public Brand Add(BrandVM brandVm)
        {
            var brand = _mapper.Map<Brand>(brandVm);
            return _brandRepository.Add(brand).Result;
        }

        public async Task<Brand> Update(BrandVM brandVm, long id)
        {
            var brand = _mapper.Map<Brand>(brandVm);
            brand.Id = id;
            return await _brandRepository.Update(brand);
        }

        public void Delete(long id)
        {
            _brandRepository.Delete(id);
        }
    }
}