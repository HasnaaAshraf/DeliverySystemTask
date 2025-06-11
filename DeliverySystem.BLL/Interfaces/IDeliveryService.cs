using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.DAL.Models;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.BLL.Interfaces
{
    public interface IDeliveryService 
    {
        List<DeliveryDto> GetDeliverySlots(List<ProductDto> productDtos, DateTime time,ProductType productType);
      
    }
}
