using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Data.EF.Repositories;
using CoolBaby.Data.Entities;
using CoolBaby.Data.Enums;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using CoolBaby.Utilities.Constants;
using CoolBaby.Utilities.Dtos;
using CoolBaby.Utilities.Helpers;

namespace CoolBaby.Application.Implementation
{
    public class ProductService : IProductService
    {
        IProductCategoryRepository _productCategoryRepository;
        IProductRepository _productRepository;
        ITagRepository _tagRepository;
        IProductTagRepository _productTagRepository;
        IProductQuantityRepository _productQuantityRepository;
        IWholePriceRepository _wholePriceRepository;

        IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository,
            ITagRepository tagRepository,
            IProductQuantityRepository productQuantityRepository,
            IWholePriceRepository wholePriceRepository,
        IUnitOfWork unitOfWork,
        IProductTagRepository productTagRepository,
        IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productQuantityRepository = productQuantityRepository;
            _productTagRepository = productTagRepository;
            _wholePriceRepository = wholePriceRepository;
            _unitOfWork = unitOfWork;
            _productCategoryRepository = productCategoryRepository;
        }

        public ProductViewModel Add(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                var product = Mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    product.ProductTags.Add(productTag);
                }
                _productRepository.Add(product);
            }
            else
            {
                var product = Mapper.Map<ProductViewModel, Product>(productVm);
                _productRepository.Add(product);
            }
            return productVm;
        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Add(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    PerfumeId = quantity.PerfumeId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity,
                    MoreImages = quantity.MoreImages
                });
            }
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll(x => x.ProductCategory).ProjectTo<ProductViewModel>().ToList();
        }

        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize, string sortBy)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (categoryId.HasValue)
            {
                if (categoryId.Value != 0)
                {
                    var lstSubCatelog = _productCategoryRepository.FindAll(x => x.ParentId == categoryId)
                        .Select(x => x.Id).ToList();
                    if (lstSubCatelog.Count == 0)
                        query = query.Where(x => x.CategoryId == categoryId.Value);
                    else
                        query = query.Where(x => lstSubCatelog.Contains(x.CategoryId));
                }
            }

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();

            switch (sortBy)
            {
                case "PriceESC":
                    query = query.OrderBy(x => x.PromotionPrice ?? x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "PriceDESC":
                    query = query.OrderByDescending(x => x.PromotionPrice ?? x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "ViewCount":
                    query = query.OrderByDescending(x => x.ViewCount)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
            }
            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            return _productQuantityRepository.FindAll(x => x.ProductId == productId).ProjectTo<ProductQuantityViewModel>().ToList();
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();
                    product.SeoKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = t;
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.Id == productVm.Id).ToList());
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
            }

            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            foreach (var productTag in productTags)
            {
                product.ProductTags.Add(productTag);
            }
            _productRepository.Update(product);
        }

        public void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices)
        {
            _wholePriceRepository.RemoveMultiple(_wholePriceRepository
                .FindAll(x => x.ProductId == productId).ToList());
            foreach (var wholePrice in wholePrices)
            {
                _wholePriceRepository.Add(new WholePrice()
                {
                    ProductId = productId,
                    FromQuantity = wholePrice.FromQuantity,
                    ToQuantity = wholePrice.ToQuantity,
                    Price = wholePrice.Price
                });
            }
        }

        public List<WholePriceViewModel> GetWholePrices(int productId)
        {
            return _wholePriceRepository.FindAll(x => x.ProductId == productId)
                .ProjectTo<WholePriceViewModel>().ToList();
        }

        public List<ProductViewModel> GetTopSellProduct(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active)
                .OrderByDescending(x => x.QuantitySold)
                .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetHotProduct(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<ProductViewModel>()
                .ToList();
        }

        public List<ProductViewModel> GetTopReviewProduct(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active)
                .OrderByDescending(x => x.ViewCount)
                .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetTopPromotionProduct(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.PromotionPrice.HasValue)
                .OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetTopPopularProduct(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.PopularFlag)
                  .OrderByDescending(x => x.DateCreated)
                  .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetRelatedProducts(int id, int top)
        {
            var product = _productRepository.FindById(id);

            return _productRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id && x.CategoryId == product.CategoryId)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<ProductViewModel>()
            .ToList();
        }

        public bool CheckAvailability(int productId, int size, int color)
        {
            var quantity = _productQuantityRepository.FindSingle(x => x.ColorId == color && x.PerfumeId == size && x.ProductId == productId);
            if (quantity == null)
                return false;
            return quantity.Quantity > 0;
        }

        public List<TagViewModel> GetProductTags(int productId)
        {
            var tags = _tagRepository.FindAll();
            var productTags = _productTagRepository.FindAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        where pt.ProductId == productId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };
            return query.ToList();

        }

        public List<ProductViewModel> GetLastest(int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<string> GetListProductByName(string name)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.Name.Contains(name))
                .Select(y => y.Name).ToList();
        }

        public PagedResult<ProductViewModel> GetSellProductPaging(int page, int pageSize, string sortBy)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active && x.PromotionPrice.HasValue);

            int totalRow = query.Count();

            switch (sortBy)
            {
                case "PriceESC":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "PriceDESC":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "ViewCount":
                    query = query.OrderByDescending(x => x.ViewCount)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
            }
            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public PagedResult<ProductViewModel> GetHotProductPaging(int page, int pageSize, string sortBy)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true);

            int totalRow = query.Count();

            switch (sortBy)
            {
                case "PriceESC":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "PriceDESC":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "ViewCount":
                    query = query.OrderByDescending(x => x.ViewCount)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
            }
            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public PagedResult<ProductViewModel> GetPopularProductPaging(int page, int pageSize, string sortBy)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active && x.PopularFlag == true);

            int totalRow = query.Count();

            switch (sortBy)
            {
                case "PriceESC":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "PriceDESC":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice.GetValueOrDefault() : x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "ViewCount":
                    query = query.OrderByDescending(x => x.ViewCount)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
            }
            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public PagedResult<ProductViewModel> GetPagingByBrand(int? categoryId, int page, int pageSize, string sortBy)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.BrandId == categoryId.Value);
            }

            int totalRow = query.Count();

            switch (sortBy)
            {
                case "PriceESC":
                    query = query.OrderBy(x => x.PromotionPrice ?? x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "PriceDESC":
                    query = query.OrderByDescending(x => x.PromotionPrice ?? x.Price)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                case "ViewCount":
                    query = query.OrderByDescending(x => x.ViewCount)
               .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);
                    break;
            }
            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }
    }
}
