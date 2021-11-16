using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
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