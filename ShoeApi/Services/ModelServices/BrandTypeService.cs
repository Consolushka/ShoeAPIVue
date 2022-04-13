using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
{
    public class BrandTypeService: IBrandTypeService
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
            return await _brandTypeRepository.Add(brandType); 
        }

        public async Task Delete(long id)
        {
            await _brandTypeRepository.Delete(id);
        }

        public async Task<BrandType> GetById(long id)
        {
            return await _brandTypeRepository.GetById(id);
        }
        
        public async Task<List<Brand>> GetBrandsByType(long id)
        {
            return await _brandTypeRepository.GetByType(id);
        }
        
        public async Task<List<Type>> GetTypesByBrand(long id)
        {
            return await _brandTypeRepository.GetByBrand(id);
        }

        public async Task<BrandType> TryToAdd(BrandType brandType)
        {
            var all = await _brandTypeRepository.GetAll();
            var matched = all.Find((bt) => bt.Brand == brandType.Brand && bt.Type == brandType.Type);
            if (matched == null)
            {
                // _brandRepository.
                return await _brandTypeRepository.Add(brandType);
            }

            return null;
        }
    }
}