using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Repository
{
    public class BaseModelProductRepository<T>: BaseRepository<T> where T:BaseProductModel
    {
        public override Task<T> Add(T entity)
        {
            if (IsExists(entity.Name))
            {
                throw new Exception($"{typeof(T).Name} with name {entity.Name} is already exists");
            }
            return base.Add(entity);
        }

        private bool IsExists(string name)
        {
            if (Context.Set<T>().FirstOrDefault(t => t.Name == name) == null)
            {
                return false;
            }

            return true;
        }
    }
}