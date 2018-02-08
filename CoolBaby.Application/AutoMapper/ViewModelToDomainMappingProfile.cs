using AutoMapper;
using System;
using CoolBaby.Application.ViewModels.Advertistment;
using CoolBaby.Application.ViewModels.Blog;
using CoolBaby.Application.ViewModels.Brand;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Data.Entities;

namespace CoolBaby.Application.AutoMapper
{
    /// <summary>
    /// View Model To Domain Mapping Profile class
    /// </summary>
    public class ViewModelToDomainMappingProfile : Profile
    {
        #region Methods
        /// <summary>
        /// View Model To Domain Mapping Profile
        /// </summary>
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Name, c.ParentId,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProductViewModel, Product>()
           .ConstructUsing(c => new Product(c.Name, c.CategoryId, c.Image, c.Price, c.OriginalPrice,
           c.PromotionPrice, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Unit, c.Status,
           c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription, c.QuantitySold, c.PopularFlag, c.BrandId));

            CreateMap<AppUserViewModel, AppUser>()
            .ConstructUsing(c => new AppUser(c.Id, c.FullName, c.UserName,
            c.Email, c.PhoneNumber, c.Avatar, c.Status, DateTime.Parse(c.BirthDay)));

            CreateMap<AppRoleViewModel, AppRole>()
           .ConstructUsing(c => new AppRole(c.Name, c.Description));

            CreateMap<PermissionViewModel, Permission>()
            .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));

            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id, c.CustomerName, c.CustomerAddress,
              c.CustomerMobile, c.CustomerMessage, c.BillStatus,
              c.PaymentMethod, c.Status, c.CustomerId));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId,
              c.Quantity, c.Price, c.ColorId, c.SizeId));

            CreateMap<BrandViewModel, Brand>()
             .ConstructUsing(c => new Brand(c.Name, c.Image, c.SortOrder, c.Status,
             c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<TagViewModel, Tag>()
           .ConstructUsing(c => new Tag(c.Name, c.Type));

            CreateMap<AdvertistmentViewModel, Advertistment>()
             .ConstructUsing(c => new Advertistment(c.Name, c.Description, c.Image, c.Url, c.PositionId,
              c.Status, c.SortOrder));

            CreateMap<BlogCategoryViewModel, BlogCategory>()
               .ConstructUsing(c => new BlogCategory(c.Name,
               c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<BlogViewModel, Blog>()
          .ConstructUsing(c => new Blog(c.Id, c.Name, c.Image,
            c.Description, c.Content, c.HotFlag, c.Tags, c.Status,
          c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription, c.CategoryId));

            CreateMap<ColorViewModel, Color>()
          .ConstructUsing(c => new Color(c.Id, c.Name, c.Code));

            CreateMap<PerfumeViewModel, Perfume>()
      .ConstructUsing(c => new Perfume(c.Id, c.Name));

            CreateMap<SizeViewModel, Size>()
  .ConstructUsing(c => new Size(c.Id, c.Name));

        }
        #endregion
    }
}