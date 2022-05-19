using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface ICheckable<T> where T: Base
    {
        Task<bool> IsAlreadyExists(T entity);
    }
}