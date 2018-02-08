using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;

namespace CoolBaby.Application.Implementation
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(ISizeRepository sizeRepository,
            IUnitOfWork unitOfWork)
        {
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
        }

        public SizeViewModel Add(SizeViewModel sizeVm)
        {
            var size = Mapper.Map<SizeViewModel, Size>(sizeVm);
            _sizeRepository.Add(size);
            return sizeVm;
        }

        public void Delete(int id)
        {
            _sizeRepository.Remove(id);
        }

        public List<SizeViewModel> GetAll()
        {
            return _sizeRepository.FindAll()
               .ProjectTo<SizeViewModel>().ToList();
        }

        public SizeViewModel GetById(int id)
        {
            return Mapper.Map<Size, SizeViewModel>(_sizeRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(SizeViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<SizeViewModel, Size>(blogCategoryVm);
            _sizeRepository.Update(blogCategory);
        }
    }
}