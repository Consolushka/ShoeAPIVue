using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class TypeRepository: BaseModelProductRepository<Type>, ITypeRepository
    {
        public TypeRepository(ShopContext context)
        {
            Context = context;
        }
    }
}