using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.ViewModels;
using Shop.Repositories.Contracts;
using Shop.Services.Contracts;
using Type = Shop.Data.Models.Type;

namespace Shop.Services.ModelServices
{
    internal class TypeService: ITypeService
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

        public async Task<Type> GetById(long id)
        {
            var type = await _repository.GetById(id);
            if (type == null)
            {
                throw new Exception($"Cannot find Type with id: {id}");
            }
            return type;
        }

        public async Task<Type> Add(TypeVM vm)
        {
            var type = _mapper.Map<Type>(vm);
            var matched = await _repository.GetSameType(type);
            if (matched != null)
            {
                throw new Exception("Same Type already exists");
            }
            if (type == null)
            {
                return null;
            }
            return await _repository.Add(type);
        }

        public async Task<Type> Update(TypeVM vm, long id)
        {
            var type = await _repository.GetById(id);
            if (type == null)
            {
                throw new Exception($"Cannot find Type with id: {id}");
            }
            if (vm == null)
            {
                return null;
            }
            type.Name = vm.Name;
            var matched = await _repository.GetSameType(type);
            if (matched != null)
            {
                throw new Exception("Same Type already exists");
            }
            return await _repository.Update(type);
        }

        public async Task Delete(long id)
        {
            var matched = await _repository.GetById(id);
            if (matched == null)
            {
                throw new Exception($"Cannot find Type with id: {id}");
            }
            await _repository.Delete(id);
        }
    }
}