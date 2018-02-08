using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;

namespace CoolBaby.Data.EF.Repositories
{
    public class PerfumeRepository : EFRepository<Perfume, int>, IPerfumeRepository
    {
        public PerfumeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
