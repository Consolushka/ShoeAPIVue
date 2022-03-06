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
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public BrandTypeService(IBrandTypeRepository brandTypeRepository/*, IBrandRepository brandRepository, ITypeRepository typeRepository*/)
        {
            _brandTypeRepository = brandTypeRepository;
            // _brandRepository = brandRepository;
            // _typeRepository = typeRepository;
        }
        
        public async Task<List<BrandType>> GetBrandsByType(long id)
        {
            var AllBT = await _brandTypeRepository.GetAll();
            return AllBT.FindAll((bt) => bt.Type.Id == id);
        }
        
        public async Task<List<BrandType>> GetTypesByBrand(long id)
        {
            var AllBT = await _brandTypeRepository.GetAll();
            return AllBT.FindAll((bt) => bt.Brand.Id == id);
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