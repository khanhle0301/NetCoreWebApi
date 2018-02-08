using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Blog;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Interfaces
{
    public interface IBlogCategoryService
    {
        BlogCategoryViewModel Add(BlogCategoryViewModel productCategoryVm);

        void Update(BlogCategoryViewModel productCategoryVm);

        void Delete(int id);

        List<BlogCategoryViewModel> GetAll();

        List<BlogCategoryViewModel> GetAll(string keyword);

        BlogCategoryViewModel GetById(int id);

        void Save();
        PagedResult<BlogCategoryViewModel> GetAllPaging(string keyword, int page, int pageSize);
    }
}