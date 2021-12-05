using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface ILogService
    {
        Task<List<Log>> GetAll();
        Task<Log> GetById(long Id);
        void Delete(long id);
    }
}