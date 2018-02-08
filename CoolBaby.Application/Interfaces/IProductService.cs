using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();

        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize, string sortBy);

        PagedResult<ProductViewModel> GetSellProductPaging(int page, int pageSize, string sortBy);

        PagedResult<ProductViewModel> GetHotProductPaging(int page, int pageSize, string sortBy);

        PagedResult<ProductViewModel> GetPopularProductPaging(int page, int pageSize, string sortBy);

        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);

        void ImportExcel(string filePath, int categoryId);


        void Save();

        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);

        void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices);

        List<WholePriceViewModel> GetWholePrices(int productId);

        List<ProductViewModel> GetTopSellProduct(int top);

        List<ProductViewModel> GetTopPromotionProduct(int top);

        List<ProductViewModel> GetHotProduct(int top);

        List<ProductViewModel> GetTopReviewProduct(int top);

        List<ProductViewModel> GetTopPopularProduct(int top);

        List<ProductViewModel> GetRelatedProducts(int id, int top);

        bool CheckAvailability(int productId, int size, int color);

        List<TagViewModel> GetProductTags(int productId);

        List<ProductViewModel> GetLastest(int top);

        List<string> GetListProductByName(string name);

        PagedResult<ProductViewModel> GetPagingByBrand(int? categoryId, int page, int pageSize, string sortBy);

    }
}
