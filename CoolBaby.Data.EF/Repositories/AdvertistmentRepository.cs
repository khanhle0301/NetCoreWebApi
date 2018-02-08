using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class AdvertistmentRepository : EFRepository<Advertistment, int>, IAdvertistmentRepository
    {
        public AdvertistmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
