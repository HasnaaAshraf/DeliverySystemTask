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
        public List<ProductDto>? ProductDto { get; set; }
        public DateTime Time { get; set; }
    }
}
