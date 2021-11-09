using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Repository.Contracts;
using Core.Contracts;

namespace Core.Services
{
    public class BrandService: IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public List<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public Brand GetById(long Id)
        {
            return _brandRepository.GetById(Id);
        }

        public Brand Add(Brand brand)
        {
            return _brandRepository.Add(brand).Result;
        }

        public async Task<Brand> Update(Brand brand)
        {
            return await _brandRepository.Update(brand);
        }

        public void Delete(long id)
        {
            _brandRepository.Delete(id);
        }
    }
}