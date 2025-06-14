using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    public class ExternalProductSlot(DateTime requestTime) : ProductSlotBase(requestTime)
    {
        protected override DateTime GetStartDay()
        {
            DateTime StartDeliveryDay = _requestTime.Date.AddDays(3);

            // Skip Saturday, Sunday, Monday
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
            // Checking if that day from Tuesday to Friday or not
            return day.DayOfWeek >= DayOfWeek.Tuesday && day.DayOfWeek <= DayOfWeek.Friday;
        }
    }
}
