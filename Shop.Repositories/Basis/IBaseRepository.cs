using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IBaseRepository<T>: IGettable<T>, IAddable<T>, IChangeable<T>, ICheckable<T> where T: Base 
    {
        
    }
}