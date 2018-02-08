using System.ComponentModel.DataAnnotations;

namespace CoolBaby.Application.ViewModels.Tag
{
    internal class TagViewModel
    {
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Type { get; set; }
    }
}