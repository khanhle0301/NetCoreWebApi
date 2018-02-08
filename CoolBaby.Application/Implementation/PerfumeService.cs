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
    public class PerfumeService : IPerfumeService
    {
        private readonly IPerfumeRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PerfumeService(IPerfumeRepository brandRepository,
            IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }

        public PerfumeViewModel Add(PerfumeViewModel brandVm)
        {
            var brand = Mapper.Map<PerfumeViewModel, Perfume>(brandVm);
            _brandRepository.Add(brand);
            return brandVm;
        }

        public void Delete(int id)
        {
            _brandRepository.Remove(id);
        }

        public List<PerfumeViewModel> GetAll()
        {
            return _brandRepository.FindAll()
               .ProjectTo<PerfumeViewModel>().ToList();
        }

        public PerfumeViewModel GetById(int id)
        {
            return Mapper.Map<Perfume, PerfumeViewModel>(_brandRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PerfumeViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<PerfumeViewModel, Perfume>(blogCategoryVm);
            _brandRepository.Update(blogCategory);
        }
    }
}