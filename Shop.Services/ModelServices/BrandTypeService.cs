using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;
using Type = Shop.Data.Models.Type;

namespace Shop.Services.ModelServices
{
    internal class BrandTypeService: IBrandTypeService
    {

        private readonly IBrandTypeRepository _brandTypeRepository;

        public BrandTypeService(IBrandTypeRepository brandTypeRepository)
        {
            _brandTypeRepository = brandTypeRepository;
        }

        public async Task<IEnumerable<BrandType>> GetAll()
        {
            return await _brandTypeRepository.GetAll();
        }

        public async Task<BrandType> Add(BrandType brandType)
        {
            var matched = await _brandTypeRepository.GetByBrandAndType(brandType.BrandId, brandType.TypeId);
            if (matched != null)
            {
                throw new Exception("Same BrandType already exists");
            }
            return await _brandTypeRepository.Add(brandType); 
        }

        public async Task Delete(long id)
        {
            var matched =await _brandTypeRepository.GetById(id);
            if (matched == null)
            {
                throw new Exception($"Cannot find BrandType with id: {id}");
            }
            await _brandTypeRepository.Delete(id);
        }

        public async Task<BrandType> GetById(long id)
        {
            var matched =await _brandTypeRepository.GetById(id);
            if (matched == null)
            {
                throw new Exception($"Cannot find BrandType with id: {id}");
            }
            return matched;
        }
        
        public async Task<List<Brand>> GetBrandsByType(long id)
        {
            var res = await _brandTypeRepository.GetByType(id);
            if (res == null)
            {
                throw new System.NullReferenceException("Cannot find this Type");
            }

            return res;
        }
        
        public async Task<List<Type>> GetTypesByBrand(long id)
        {
            var res = await _brandTypeRepository.GetByBrand(id);
            if (res == null)
            {
                throw new System.NullReferenceException("Cannot find this Brand");
            }

            return res;
        }
    }
}