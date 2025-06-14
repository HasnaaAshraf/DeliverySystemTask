using DeliverySystem.DAL.Models;

namespace DeliverySystemTask.DTOS
{
    public class ProductDto
    {
        public int Quantity { get; set; }
        public ProductType ProductType { get; set; }
    }
}
