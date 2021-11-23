using System;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Repository.Contracts;

namespace WebApplication.Repository.EntityRepository
{
    public class LogRepository: BaseRepository<Log>, ILogRepository
    {
        public LogRepository(ShoeContext context)
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