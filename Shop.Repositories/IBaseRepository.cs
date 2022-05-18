using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Repositories
{
    public interface IBaseRepository<T> where T:Base
    {
        Task<List<T>> GetAll();
        Task<T> GetById(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(long id);
        Task<bool> IsAlreadyExists(T entity);
        Task CheckForExistingId(long id);
    }
}