using Entities.Models;

namespace Repository
{
    public interface IBrandRepository: IBaseRepository<Brand>
    {
        Brand Update(Brand brand);
        void Delete(long brand);
    }
}