using Entities.Models;

namespace Repository.Contracts
{
    public interface IShoeRepository: IBaseRepository<Shoe>
    {
        Shoe Update(Shoe shoe);
        void Delete(long id);
    }
}