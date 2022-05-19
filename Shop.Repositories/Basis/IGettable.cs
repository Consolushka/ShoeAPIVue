using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IGettable<T> where T:Base
    {
        Task<List<T>> GetAll();
        Task<T> GetById(long id);
    }
}