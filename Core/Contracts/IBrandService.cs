using System.Collections.Generic;
using Entities.Models;

namespace Core.Contracts
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(long Id);
        Brand Add(Brand brand);
        Brand Update(Brand brand);
        void Delete(long id);
    }
}