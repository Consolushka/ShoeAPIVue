using System.Collections.Generic;
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

        public List<Log> GetAll()
        {
            return _repository.GetAll();
        }

        public Log GetById(long Id)
        {
            return _repository.GetById(Id);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}