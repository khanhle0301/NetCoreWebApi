using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class WholePriceRepository : EFRepository<WholePrice, int>, IWholePriceRepository
    {
        public WholePriceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
