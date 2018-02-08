using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Blog;
using CoolBaby.Data.Entities;
using CoolBaby.Data.Enums;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Implementation
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private IBlogCategoryRepository _blogCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public BlogCategoryViewModel Add(BlogCategoryViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<BlogCategoryViewModel, BlogCategory>(blogCategoryVm);
            _blogCategoryRepository.Add(blogCategory);
            return blogCategoryVm;
        }

        public void Delete(int id)
        {
            _blogCategoryRepository.Remove(id);
        }

        public List<BlogCategoryViewModel> GetAll()
        {
            return _blogCategoryRepository.FindAll().OrderByDescending(x => x.DateCreated)
                 .ProjectTo<BlogCategoryViewModel>().ToList();
        }

        public List<BlogCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _blogCategoryRepository.FindAll(x => x.Name.Contains(keyword))
                    .ProjectTo<BlogCategoryViewModel>().ToList();
            else
                return _blogCategoryRepository.FindAll().OrderByDescending(x => x.DateCreated)
                    .ProjectTo<BlogCategoryViewModel>()
                    .ToList();
        }

        public PagedResult<BlogCategoryViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _blogCategoryRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .ProjectTo<BlogCategoryViewModel>().ToList();

            var paginationSet = new PagedResult<BlogCategoryViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public BlogCategoryViewModel GetById(int id)
        {
            return Mapper.Map<BlogCategory, BlogCategoryViewModel>(_blogCategoryRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(BlogCategoryViewModel blogCategoryVm)
        {
            var blogCategory = Mapper.Map<BlogCategoryViewModel, BlogCategory>(blogCategoryVm);
            _blogCategoryRepository.Update(blogCategory);
        }
    }
}