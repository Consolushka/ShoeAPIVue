using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IDeletable<T> where T:Base
    {
        Task Delete(long id);
    }
}