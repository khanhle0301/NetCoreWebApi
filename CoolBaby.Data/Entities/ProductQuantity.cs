using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("ProductQuantities")]
    public class ProductQuantity : DomainEntity<int>
    {
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Column(Order = 2)]
        public int? PerfumeId { get; set; }

        [Column(Order = 3)]
        public int? ColorId { get; set; }

        [Column(Order = 4)]
        public int? SizeId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("PerfumeId")]
        public virtual Perfume Perfume { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }
    }
}