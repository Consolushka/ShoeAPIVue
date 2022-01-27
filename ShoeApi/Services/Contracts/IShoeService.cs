using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
{
    public interface IShoeService
    {
        Task<List<Shoe>> GetAll();
        Task<Shoe> GetById(long id);
        Task<Shoe> Add(ShoeVM shoe);
        Task<Shoe> Update(ShoeVM shoe, long id);
        Task Delete(long id);
    }
}