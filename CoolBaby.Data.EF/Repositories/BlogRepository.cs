using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Data.Enums;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class BlogRepository : EFRepository<Blog, int>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }
    }
}
