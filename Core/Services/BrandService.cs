using System.Collections.Generic;
using Entities.Models;
using Repository;
using Repository.EntityRepository;

namespace Core
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

        public long Add(Brand brand)
        {
            return _brandRepository.Add(brand);
        }

        public Brand Update(Brand brand)
        {
            return _brandRepository.Update(brand);
        }

        public void Delete(long id)
        {
            _brandRepository.Delete(id);
        }
    }
}