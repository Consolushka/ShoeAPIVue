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

        public Brand Update(Brand brand)
        {
            var res = Context.Brand.Update(brand);
            Context.SaveChanges();
            return res.Entity;
        }

        public void Delete(long id)
        {
            var brand = GetById(id);
            Context.Brand.Remove(brand);
            Context.SaveChanges();
        }
    }
}