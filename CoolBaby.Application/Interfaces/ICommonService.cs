using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Advertistment;
using CoolBaby.Application.ViewModels.Common;
using CoolBaby.Application.ViewModels.Product;

namespace CoolBaby.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();

        List<SlideViewModel> GetSlides();

        SystemConfigViewModel GetSystemConfig(string code);

        List<TagViewModel> GetTagByType(string type);

        List<ColorViewModel> GetColorByProduct(int productId);

        List<SizeViewModel> GetSizeByProduct(int productId);

        List<PerfumeViewModel> GetPerfumeByProduct(int productId);

        List<AdvertistmentViewModel> GetAdvertistmentByPositionId(string positionId);

        List<AdvertistmentViewModel> GetAdvertistments();

        ProductQuantityViewModel GetQuantities(int productId, int colorId, int sizeId, int perfumeId);
    }
}