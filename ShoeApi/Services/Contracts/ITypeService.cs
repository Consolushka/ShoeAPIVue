using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;

namespace WebApplication.Services.Contracts
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