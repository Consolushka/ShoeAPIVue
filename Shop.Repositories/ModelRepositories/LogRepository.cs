using System;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.Contracts;

namespace Shop.Repositories.ModelRepositories
{
    internal class LogRepository: BaseRepository<Log>, ILogRepository
    {
        public LogRepository(ShopContext context)
        {
            Context = context;
        }

        public new Task<Log> Add(Log log)
        {
            throw new Exception("There is no way to add new Log");
        }
        public new Task<Log> Update(Log log)
        {
            throw new Exception("There is no way to update existing Log");
        }
    }
}