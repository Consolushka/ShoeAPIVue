using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Core.Contracts
{
    public interface IShoeService
    {
        List<Shoe> GetAll();
        Shoe GetById(long Id);
        Shoe Add(Shoe shoe);
        Task<Shoe> Update(Shoe shoe);
        void Delete(long id);
    }
}