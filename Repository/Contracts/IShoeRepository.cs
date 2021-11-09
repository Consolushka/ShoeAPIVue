using Entities.Models;

namespace Repository.Contracts
{
    public interface IShoeRepository: IBaseRepository<Shoe>
    {
        void Delete(long id);
    }
}