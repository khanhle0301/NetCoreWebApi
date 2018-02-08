using System.Collections.Generic;

namespace CoolBaby.Application.ViewModels.Advertistment
{
    public class AdvertistmentPageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<AdvertistmentPositionViewModel> AdvertistmentPositions { get; set; }
    }
}