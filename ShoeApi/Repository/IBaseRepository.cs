using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Repository
{
    public interface IBaseRepository<T> where T:Base
    {
        List<T> GetAll();
        T GetById(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(long id);
    }
}