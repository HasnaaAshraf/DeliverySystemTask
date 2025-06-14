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
        public DeliveryService() { }
   
        public List<DeliveryDto> GetDeliverySlots(List<ProductDto> productDtos, DateTime requestTime)
        {
            List<List<DeliveryDto>> productSlotsList = [];

            foreach (var product in productDtos)
            {
                // Choose the correct slot based on ProductType
                ISlots? strategy = product.ProductType switch
                {
                    ProductType.External => new ExternalProductSlot(requestTime),
                    ProductType.InStock => new InStockProductSlot(requestTime),
                    ProductType.Fresh => new FreshProductSlot(requestTime),
                    _ => null
                };

                if (strategy == null) continue;

                List<DeliveryDto> slots = strategy.GetSlots();

                if (slots.Count == 0)
                    continue; 

                productSlotsList.Add(slots);
            }

            if (productSlotsList.Count == 0)
                return [];

           
            HashSet<string> commonSlots = productSlotsList[0]
                .Select(s => s.Start + "-" + s.End)
                .ToHashSet();

            // Find the common slots 
            for (int i = 1; i < productSlotsList.Count; i++)
            {
                var commonSlot = productSlotsList[i]
                    .Select(s => s.Start + "-" + s.End)
                    .ToHashSet();

                commonSlots.IntersectWith(commonSlot);
            }

            if (commonSlots.Count == 0)
                return [];

            // Filter the product’s slots with the common slots
            List<DeliveryDto> validSlots = productSlotsList[0]
                             .Where(s => commonSlots.Contains(s.Start + "-" + s.End))
                             .ToList();

            return validSlots;
        }

    }
}
