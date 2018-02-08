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
    public class ColorService : IColorService
    {
        private readonly IProductQuantityRepository _productQuantityRepository;
        private readonly IColorRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IColorRepository brandRepository,
            IUnitOfWork unitOfWork,
            IProductQuantityRepository productQuantityRepository)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _productQuantityRepository = productQuantityRepository;
        }

        public ColorViewModel Add(ColorViewModel brandVm)
        {
            var brand = Mapper.Map<ColorViewModel, Color>(brandVm);
            _brandRepository.Add(brand);
            return brandVm;
        }

        public void Delete(int id)
        {
            _brandRepository.Remove(id);
        }

        public List<ColorViewModel> GetAll()
        {
            return _brandRepository.FindAll()
               .ProjectTo<ColorViewModel>().ToList();
        }


        public ColorViewModel GetById(int id)
        {
            return Mapper.Map<Color, ColorViewModel>(_brandRepository.FindById(id));
        }

        public List<ColorViewModel> GetColorByProduct(int productId)
        {
            var query = (from t in _productQuantityRepository.FindAll()
                         join pt in _brandRepository.FindAll()
                         on t.ColorId equals pt.Id
                         select pt);
            return query.ProjectTo<ColorViewModel>().ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ColorViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<ColorViewModel, Color>(blogCategoryVm);
            _brandRepository.Update(blogCategory);
        }
    }
}