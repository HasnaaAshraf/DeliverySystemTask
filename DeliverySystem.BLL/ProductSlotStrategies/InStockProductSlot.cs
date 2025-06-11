using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    internal class InStockProductSlot : ProductSlotBase
    {
        protected override DateTime GetStartDay()
        {
            return DateTime.Now.AddDays(1);
        }

        protected override bool IsDeliveryDay(DateTime day)
        {
            return day.DayOfWeek != DayOfWeek.Sunday &&
                   day.DayOfWeek != DayOfWeek.Sunday ;                       
        }
    }
}
