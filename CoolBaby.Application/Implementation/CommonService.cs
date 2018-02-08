using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Advertistment;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using CoolBaby.Utilities.Constants;

namespace CoolBaby.Application.Implementation
{
    public class CommonService : ICommonService
    {
        private readonly IProductQuantityRepository _productQuantityRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IPerfumeRepository _perfumeRepository;
        private readonly IAdvertistmentRepository _advertistmentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IFooterRepository _footerRepository;
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlideRepository _slideRepository;

        public CommonService(IFooterRepository footerRepository,
            ISystemConfigRepository systemConfigRepository,
            IUnitOfWork unitOfWork,
            ISlideRepository slideRepository,
            ITagRepository tagRepository,
            IAdvertistmentRepository advertistmentRepository,
            IProductQuantityRepository productQuantityRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IPerfumeRepository perfumeRepository)
        {
            _perfumeRepository = perfumeRepository;
            _sizeRepository = sizeRepository;
            _productQuantityRepository = productQuantityRepository;
            _colorRepository = colorRepository;
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
            _tagRepository = tagRepository;
            _advertistmentRepository = advertistmentRepository;
        }

        public List<AdvertistmentViewModel> GetAdvertistmentByPositionId(string positionId)
        {
            return _advertistmentRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.PositionId == positionId)
              .ProjectTo<AdvertistmentViewModel>().ToList();
        }

        public List<AdvertistmentViewModel> GetAdvertistments()
        {
            return _advertistmentRepository.FindAll(x => x.Status == Data.Enums.Status.Active)
               .ProjectTo<AdvertistmentViewModel>().ToList();
        }

        public List<ColorViewModel> GetColorByProduct(int productId)
        {
            var query = (from t in _productQuantityRepository.FindAll()
                         join pt in _colorRepository.FindAll()
                         on t.ColorId equals pt.Id
                         where t.ProductId == productId
                         select pt).Distinct();
            return query.ProjectTo<ColorViewModel>().ToList();
        }

        public FooterViewModel GetFooter()
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<PerfumeViewModel> GetPerfumeByProduct(int productId)
        {
            var query = (from t in _productQuantityRepository.FindAll()
                         join pt in _perfumeRepository.FindAll()
                         on t.PerfumeId equals pt.Id
                         where t.ProductId == productId
                         select pt).Distinct();
            return query.ProjectTo<PerfumeViewModel>().ToList();
        }

        public ProductQuantityViewModel GetQuantities(int productId, int colorId, int sizeId, int perfumeId)
        {
            var query = _productQuantityRepository.
                FindSingle(x => x.ProductId == productId
                && x.ColorId == colorId && x.SizeId == sizeId && x.PerfumeId == perfumeId);
            return Mapper.Map<ProductQuantity, ProductQuantityViewModel>(query);
        }

        public List<SizeViewModel> GetSizeByProduct(int productId)
        {
            var query = (from t in _productQuantityRepository.FindAll()
                         join pt in _sizeRepository.FindAll()
                         on t.SizeId equals pt.Id
                         where t.ProductId == productId
                         select pt).Distinct();
            return query.ProjectTo<SizeViewModel>().ToList();
        }

        public List<SlideViewModel> GetSlides()
        {
            return _slideRepository.FindAll(x => x.Status)
                .ProjectTo<SlideViewModel>().ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return Mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }

        public List<TagViewModel> GetTagByType(string type)
        {
            return _tagRepository.FindAll(x => x.Type == type)
                  .ProjectTo<TagViewModel>().ToList();
        }
    }
}