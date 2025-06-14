using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    // This class is used to ensure that each product type implements its own delivery constraints and slot generation logic.
    public abstract class ProductSlotBase : ISlots
    {
        public readonly DateTime _requestTime;

        protected ProductSlotBase(DateTime requestTime)
        {
            _requestTime = requestTime.ToLocalTime(); 
        }
        protected abstract DateTime GetStartDay();

        protected abstract bool IsDeliveryDay(DateTime day);

        public List<DeliveryDto> GetSlots()
        {
            List<DeliveryDto> result = [];

            DateTime currentDay = GetStartDay();

            int validDaysCount = 0;

            // Get slots for 14 delivery days
            while (validDaysCount < 14)
            {
                // Checking is that avaible day to Delivery 
                if (!IsDeliveryDay(currentDay))
                {
                    currentDay = currentDay.AddDays(1);
                    continue;
                }

                List<DeliveryDto> slots = CreateSlotsForOneDay(currentDay);

                result.AddRange(slots);

                currentDay = currentDay.AddDays(1);

                validDaysCount++;
            }

            return result;
        }

        protected List<DeliveryDto> CreateSlotsForOneDay(DateTime day)
        {
            var slots = new List<DeliveryDto>();

            // Create slots from 8:00 AM to 10:00 PM (14 slots in total)
            for (int hour = 8; hour < 22; hour++)
            {
                var slot = new DeliveryDto
                {
                    Start = new DateTime(day.Year, day.Month, day.Day, hour, 0, 0),
                    End = new DateTime(day.Year, day.Month, day.Day, hour + 1, 0, 0),

                    // Green slot = preferred delivery hours (1–3 PM, 8–10 PM)
                    GreenSlot = (hour >= 13 && hour < 15) || (hour >= 20 && hour < 22) 
                };
                slots.Add(slot);
            }
            return slots;
        }

    }
}
