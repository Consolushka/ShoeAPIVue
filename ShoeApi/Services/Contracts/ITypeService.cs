using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Services.Contracts
{
    public interface ITypeService
    {
        Task<List<Type>> GetAll();
        Task<Type> GetById(long id);
        Task<Type> Add(Type t);
        Task<Type> Update(Type t);
        void Delete(long id);
    }
}