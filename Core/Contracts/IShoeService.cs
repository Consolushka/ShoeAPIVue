using System.Collections.Generic;
using Entities.Models;

namespace Core
{
    public interface IShoeService
    {
        List<Shoe> GetAll();
        Shoe GetById(long Id);
        long Add(Shoe shoe);
        Shoe Update(Shoe shoe);
        void Delete(long id);
    }
}