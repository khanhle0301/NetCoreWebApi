using CoolBaby.Data.Entities;
using CoolBaby.Infrastructure.Interfaces;

namespace CoolBaby.Data.IRepositories
{
    public interface IBlogCategoryRepository : IRepository<BlogCategory, int>
    {
    }
}