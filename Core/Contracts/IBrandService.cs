using System.Collections.Generic;
using Entities.Models;

namespace Core
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(long Id);
        long Add(Brand brand);
        Brand Update(Brand brand);
        void Delete(long id);
    }
}