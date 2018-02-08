using AutoMapper;
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
    /// Domain To View Model Mapping Profile class
    /// </summary>
    public class DomainToViewModelMappingProfile : Profile
    {
        #region Methods
        /// <summary>
        /// Domain To ViewModel Mapping Profile
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<Function, FunctionViewModel>();
            CreateMap<Permission, PermissionViewModel>().MaxDepth(2);
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<Perfume, PerfumeViewModel>();
            CreateMap<ProductQuantity, ProductQuantityViewModel>().MaxDepth(2);
            CreateMap<WholePrice, WholePriceViewModel>().MaxDepth(2);

            CreateMap<Blog, BlogViewModel>().MaxDepth(2);
            CreateMap<BlogCategory, BlogCategoryViewModel>().MaxDepth(2);

            CreateMap<BlogTag, BlogTagViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);
            CreateMap<Brand, BrandViewModel>().MaxDepth(2);
            CreateMap<Tag, TagViewModel>().MaxDepth(2);
            CreateMap<Advertistment, AdvertistmentViewModel>().MaxDepth(2);
            CreateMap<AdvertistmentPage, AdvertistmentPageViewModel>().MaxDepth(2);
            CreateMap<AdvertistmentPosition, AdvertistmentPositionViewModel>().MaxDepth(2);
        }
        #endregion
    }
}