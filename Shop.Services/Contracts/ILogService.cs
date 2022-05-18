using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Contracts
{
    public interface ILogService
    {
        Task<List<Log>> GetAll();
        Task<Log> GetById(long Id);
        void Delete(long id);
    }
}