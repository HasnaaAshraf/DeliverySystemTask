using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Application.ProductSlotStrategies;
using DeliverySystem.BLL.Interfaces;
using DeliverySystem.DAL.Models;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.BLL.Services
{
    public class DeliveryService : IDeliveryService
    {

        public List<DeliveryDto> GetDeliverySlots(List<ProductDto> productDtos, DateTime time,ProductType productType)
        {
            ISlots strategy;

            if (productType == ProductType.External)
            {
                strategy = new ExternalProductSlot();
            }
            else if (productType == ProductType.InStock)
            {
                strategy = new InStockProductSlot();
            }
            else if (productType == ProductType.Fresh)
            {
                strategy = new FreshProductSlot();
            }
            else
            {
                return new List<DeliveryDto>();
            }

            return strategy.GetSlots();

        }
    }
}
