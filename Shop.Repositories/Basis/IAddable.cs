using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IAddable<T> where T:Base
    {
        Task<T> Add(T entity);
    }
}