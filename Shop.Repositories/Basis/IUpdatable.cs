using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IUpdatable<T> where T:Base
    {
        Task<T> Update(T entity);
    }
}