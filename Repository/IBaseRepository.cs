using System.Collections.Generic;
using Entities;

namespace Repository
{
    public interface IBaseRepository<T> where T:Base
    {
        List<T> GetAll();
        T GetById(long Id);
        long Add(T entity);
    }
}