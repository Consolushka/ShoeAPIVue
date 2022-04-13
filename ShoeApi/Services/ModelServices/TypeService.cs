using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Repository.Contracts;
using WebApplication.Services.Contracts;

namespace WebApplication.Services.ModelServices
{
    public class TypeService: ITypeService
    {
        private readonly ITypeRepository _repository;
        private readonly IMapper _mapper;

        public TypeService(ITypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Type>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Type> GetById(long Id)
        {
            return await _repository.GetById(Id);
        }

        public async Task<Type> Add(TypeVM vm)
        {
            var type = _mapper.Map<Type>(vm);
            if (type == null)
            {
                return null;
            }
            return await _repository.Add(type);
        }

        public async Task<Type> Update(TypeVM vm, long id)
        {
            var type = await GetById(id);
            if (vm == null)
            {
                return null;
            }
            type.Name = vm.Name;
            return await _repository.Update(type);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }
    }
}