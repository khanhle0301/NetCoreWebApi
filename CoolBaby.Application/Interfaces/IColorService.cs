using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Product;

namespace CoolBaby.Application.Interfaces
{
    public interface IColorService
    {
        ColorViewModel Add(ColorViewModel product);

        void Update(ColorViewModel product);

        void Delete(int id);

        List<ColorViewModel> GetAll();

        List<ColorViewModel> GetColorByProduct(int productId);

        ColorViewModel GetById(int id);

        void Save();
    }
}