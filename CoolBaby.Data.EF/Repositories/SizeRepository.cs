using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class SizeRepository : EFRepository<Size, int>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context)
        {
        }
    }
}