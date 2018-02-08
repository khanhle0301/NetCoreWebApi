using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Application.ViewModels.Blog;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel Add(BlogViewModel product);

        void Update(BlogViewModel product);

        void Delete(int id);

        List<BlogViewModel> GetAll();

        PagedResult<BlogViewModel> GetAllPaging(string keyword, int pageSize, int page);

        PagedResult<BlogViewModel> GetAllPaging(int categoryId, int pageSize, int page);

        List<BlogViewModel> GetLastest(int top);

        List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> GetList(string keyword);

        List<BlogViewModel> GetReatedBlogs(int id, int top);

        List<BlogViewModel> GetTopHotBlogs(int top);

        List<string> GetListByName(string name);

        BlogViewModel GetById(int id);

        void Save();

        List<TagViewModel> GetListTagById(int id);

        TagViewModel GetTag(string tagId);

        void IncreaseView(int id);

        PagedResult<BlogViewModel> GetListByTag(string tagId, int pageSize, int page = 1);

        List<TagViewModel> GetListTag(string searchText);
    }
}
