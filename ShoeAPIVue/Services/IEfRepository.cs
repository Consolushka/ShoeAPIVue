using System.Collections.Generic;
using System.Threading.Tasks;
using ShoeAPIVue.Entities;

namespace ShoeAPIVue.Services
{
    public interface IEfRepository<T> where T:Base
    {
        List<T> GetAll();
        T GetById(long id);
        Task<long> Add(T entity);
    }
}