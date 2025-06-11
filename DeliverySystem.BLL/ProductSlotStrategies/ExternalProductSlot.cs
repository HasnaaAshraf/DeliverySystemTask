using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    public class ExternalProductSlot : ProductSlotBase
    {
       
        protected override DateTime GetStartDay()
        {

            DateTime StartDeliveryDay = DateTime.Now.AddDays(3);

            // Skip Saturday, Sunday, Monday => For Only External Products 
            while (StartDeliveryDay.DayOfWeek == DayOfWeek.Saturday ||
                  StartDeliveryDay.DayOfWeek == DayOfWeek.Sunday ||
                  StartDeliveryDay.DayOfWeek == DayOfWeek.Monday)
            {
                StartDeliveryDay = StartDeliveryDay.AddDays(1);
            }
            return StartDeliveryDay;
        }

        protected override bool IsDeliveryDay(DateTime day)
        {
            // Tuesday Or After That And Friday Or Before 
            return day.DayOfWeek >= DayOfWeek.Tuesday && day.DayOfWeek <= DayOfWeek.Friday;
        }
    }
}
