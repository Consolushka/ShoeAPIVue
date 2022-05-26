using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Repositories.Basis
{
    public interface IBaseRepository<T>: IGettable<T>, IAddable<T>, IUpdatable<T>, IDeletable<T> where T: Base 
    {
        
    }
}