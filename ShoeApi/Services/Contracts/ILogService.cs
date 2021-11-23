using System.Collections.Generic;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface ILogService
    {
        List<Log> GetAll();
        Log GetById(long Id);
        void Delete(long id);
    }
}