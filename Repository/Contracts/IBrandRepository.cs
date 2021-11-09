using Entities.Models;

namespace Repository.Contracts
{
    public interface IBrandRepository: IBaseRepository<Brand>
    {
        void Delete(long brand);
    }
}