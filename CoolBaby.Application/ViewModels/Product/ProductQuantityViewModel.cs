namespace CoolBaby.Application.ViewModels.Product
{
    public class ProductQuantityViewModel
    {
        public int ProductId { get; set; }

        public int? SizeId { get; set; }

        public int? PerfumeId { get; set; }

        public int? ColorId { get; set; }

        public int Quantity { get; set; }

        public string MoreImages { set; get; }

        public ProductViewModel Product { get; set; }

        public PerfumeViewModel Perfume { get; set; }

        public SizeViewModel Size { get; set; }

        public ColorViewModel Color { get; set; }
    }
}