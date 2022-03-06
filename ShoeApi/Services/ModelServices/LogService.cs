using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;
using Log = WebApplication.Data.Models.Log;

namespace WebApplication.Services.ModelServices
{
    public class LogService: ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Log>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Log> GetById(long Id)
        {
            return await _repository.GetById(Id);
        }

        public void Delete(long id)
        {
            _repository.Delete(new Log(){Id=id});
        }
    }
}