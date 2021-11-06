using Entities.Models;
using Repository.Contracts;

namespace Repository.EntityRepository
{
    public class ShoeRepository: BaseRepository<Shoe>, IShoeRepository
    {

        public ShoeRepository(ShoeContext context)
        {
            Context = context;
        }

        public Shoe Update(Shoe shoe)
        {
            var res = Context.Shoe.Update(shoe);
            Context.SaveChanges();
            return res.Entity;
        }

        public void Delete(long id)
        {
            var shoe = GetById(id);
            Context.Shoe.Remove(shoe);
            Context.SaveChanges();
        }
    }
}