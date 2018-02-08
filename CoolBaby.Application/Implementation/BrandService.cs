using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Brand;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository,
            IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }

        public BrandViewModel Add(BrandViewModel brandVm)
        {
            var brand = Mapper.Map<BrandViewModel, Brand>(brandVm);
            _brandRepository.Add(brand);
            return brandVm;
        }

        public void Delete(int id)
        {
            _brandRepository.Remove(id);
        }

        public List<BrandViewModel> GetAll()
        {
            return _brandRepository.FindAll().OrderBy(x => x.SortOrder)
               .ProjectTo<BrandViewModel>().ToList();
        }

        public PagedResult<BrandViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _brandRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<BrandViewModel>().ToList();

            var paginationSet = new PagedResult<BrandViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public List<BrandViewModel> GetBrands()
        {
            return _brandRepository.FindAll(x => x.Status == Data.Enums.Status.Active)
                .OrderBy(x => x.SortOrder)
               .ProjectTo<BrandViewModel>().ToList();
        }

        public BrandViewModel GetById(int id)
        {
            return Mapper.Map<Brand, BrandViewModel>(_brandRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(BrandViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<BrandViewModel, Brand>(blogCategoryVm);
            _brandRepository.Update(blogCategory);
        }
    }
}