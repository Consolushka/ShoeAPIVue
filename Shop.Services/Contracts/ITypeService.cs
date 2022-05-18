using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.ViewModels;
using Shop.Data.Models;
using Type = Shop.Data.Models.Type;

namespace Shop.Services.Contracts
{
    public interface ITypeService
    {
        Task<List<Type>> GetAll();
        Task<Type> GetById(long id);
        Task<Type> Add(TypeVM t);
        Task<Type> Update(TypeVM t, long id);
        Task Delete(long id);
    }
}