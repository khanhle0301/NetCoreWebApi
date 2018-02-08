using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Data.Entities;
using CoolBaby.Infrastructure.Interfaces;

namespace CoolBaby.Data.IRepositories
{
    public interface ISlideRepository : IRepository<Slide, int>
    {
    }
}
