using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;

namespace Shop.Services.ModelServices
{
    internal class LogService: ILogService
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
            _repository.Delete(id);
        }
    }
}