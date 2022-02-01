using WebApplication.Data;
using WebApplication.Data.Models;

namespace WebApplication.Repository.EntityRepository
{
    public class BrandTypeRepository: BaseRepository<BrandType>
    {
        public BrandTypeRepository(ShopContext context)
        {
            Context = context;
        }
    }
}