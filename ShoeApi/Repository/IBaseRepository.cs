using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Repository
{
    public interface IBaseRepository<T> where T:Base
    {
        Task<List<T>> GetAll();
        Task<T> GetById(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<bool> IsExists(string name);
    }
}