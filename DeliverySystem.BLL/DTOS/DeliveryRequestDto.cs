using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.DAL.Models;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.Application.DTOS
{
    public class DeliveryRequestDto
    {
        public List<ProductDto> productDto { get; set; }
        public DateTime time { get; set; }
        public ProductType productType { get; set; }
    }
}
