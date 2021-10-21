using Entities.Models;

namespace Repository
{
    public interface IShoeRepository: IBaseRepository<Shoe>
    {
        Shoe Update(Shoe shoe);
        void Delete(long id);
    }
}