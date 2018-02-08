using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Product;

namespace CoolBaby.Application.Interfaces
{
    public interface ISizeService
    {
        SizeViewModel Add(SizeViewModel product);

        void Update(SizeViewModel product);

        void Delete(int id);

        List<SizeViewModel> GetAll();

        SizeViewModel GetById(int id);

        void Save();
    }
}