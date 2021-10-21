using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Repository
{
    public interface IBaseRepository<T> where T:Base
    {
        List<T> GetAll();
        T GetById(long id);
        Task<long> Add(T entity);
    }
}