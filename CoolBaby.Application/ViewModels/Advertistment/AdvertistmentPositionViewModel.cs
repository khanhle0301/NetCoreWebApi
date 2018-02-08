using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoolBaby.Application.ViewModels.Advertistment
{
    public class AdvertistmentPositionViewModel
    {
        [StringLength(20)]
        public string Id { get; set; }

        [StringLength(20)]
        public string PageId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public AdvertistmentPageViewModel AdvertistmentPage { get; set; }

        public ICollection<AdvertistmentViewModel> Advertistments { get; set; }
    }
}