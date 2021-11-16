using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandRepository: BaseRepository<Brand>, IBrandRepository
    {   
        public BrandRepository(ShoeContext context)
        {
            Context = context;
        }
    }
}