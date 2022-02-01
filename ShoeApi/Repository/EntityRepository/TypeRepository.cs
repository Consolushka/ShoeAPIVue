using WebApplication.Data;
using WebApplication.Data.Models;

namespace WebApplication.Repository.EntityRepository
{
    public class TypeRepository: BaseModelProductRepository<Type>
    {
        public TypeRepository(ShopContext context)
        {
            Context = context;
        }
    }
}