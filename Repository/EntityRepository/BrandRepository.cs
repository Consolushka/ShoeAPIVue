using Entities.Models;
using Repository.Contracts;

namespace Repository.EntityRepository
{
    public class BrandRepository: BaseRepository<Brand>, IBrandRepository
    {   
        public BrandRepository(ShoeContext context)
        {
            Context = context;
        }

        public void Delete(long id)
        {
            var brand = GetById(id);
            Context.Brand.Remove(brand);
            Context.SaveChanges();
        }
    }
}