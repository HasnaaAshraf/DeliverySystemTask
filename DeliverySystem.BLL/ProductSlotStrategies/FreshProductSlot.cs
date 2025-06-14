using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    public class FreshProductSlot(DateTime requestTime) : ProductSlotBase(requestTime)
    {
        protected override DateTime GetStartDay()
        {
            // Eligible for same day delivery if ordered before 12:00.
            return _requestTime.Hour < 12
                ? _requestTime.Date
                : _requestTime.Date.AddDays(1);
        }

        protected override bool IsDeliveryDay(DateTime day)
        {
            // Checking if that day from Monday to Friday or not
            return day.DayOfWeek != DayOfWeek.Saturday &&
                   day.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
