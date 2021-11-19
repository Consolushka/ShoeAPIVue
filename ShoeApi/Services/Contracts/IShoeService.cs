using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IShoeService
    {
        List<Shoe> GetAll();
        Shoe GetById(long Id);
        Shoe Add(ShoeVM shoe);
        Task<Shoe> Update(ShoeVM shoe, long id);
        void Delete(long id);
    }
}