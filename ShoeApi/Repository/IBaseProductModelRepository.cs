using WebApplication.Data.Models;

namespace WebApplication.Repository
{
    public interface IBaseProductModelRepository: IBaseRepository<BaseProductModel>
    {
        bool IsExists(string name);
    }
}