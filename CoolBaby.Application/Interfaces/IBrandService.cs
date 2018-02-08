using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Brand;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Interfaces
{
    public interface IBrandService
    {
        BrandViewModel Add(BrandViewModel product);

        void Update(BrandViewModel product);

        void Delete(int id);

        List<BrandViewModel> GetAll();

        List<BrandViewModel> GetBrands();

        BrandViewModel GetById(int id);

        PagedResult<BrandViewModel> GetAllPaging(string keyword, int page, int pageSize);

        void Save();
    }
}