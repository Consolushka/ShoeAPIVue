using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IChangeable<T> where T:Base
    {
        Task<T> Update(T entity);
        Task Delete(long id);
        Task<bool> IsAlreadyExists(T entity);
        Task CheckForExistingId(long id);
    }
}