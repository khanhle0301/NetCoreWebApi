using System.ComponentModel.DataAnnotations;

namespace CoolBaby.Application.ViewModels.Product
{
    public class PerfumeViewModel
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name
        {
            get; set;
        }
    }
}