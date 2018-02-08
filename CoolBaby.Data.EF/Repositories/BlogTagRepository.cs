using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class BlogTagRepository : EFRepository<BlogTag, int>, IBlogTagRepository
    {
        public BlogTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
