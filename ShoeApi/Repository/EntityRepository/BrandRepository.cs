using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandRepository: BaseModelProductRepository<Brand>, IBrandRepository
    {   
        public BrandRepository(ShopContext context)
        {
            Context = context;
        }
    }
}