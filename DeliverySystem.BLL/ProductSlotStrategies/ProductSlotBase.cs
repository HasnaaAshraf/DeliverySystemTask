using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Application.Interfaces;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.Application.ProductSlotStrategies
{
    // This is a base class for all product delivery slot strategies
    // We Make abstract Class Bec We Didn't Know Details Of Every Product Class So Every Product Must Implement ItSelf
    public abstract class ProductSlotBase : ISlots
    {
        // This method returns all delivery slots for 14 delivery days
        public List<DeliveryDto> GetSlots()
        {
            var result = new List<DeliveryDto>();

            // Start from the first delivery day (each product decides its own)
            DateTime currentDay = GetStartDay();

            // Loop until we get 14 valid delivery days (not weekends or holidays)
            for (int i = 0; i < 14; i++)
            {
                // If the current day is not allowed for delivery, skip it
                if (!IsDeliveryDay(currentDay))
                {
                    currentDay = currentDay.AddDays(1);
                    continue;
                }

                // Create hourly delivery slots for this day
                var slots = CreateSlotsForOneDay(currentDay);
                result.AddRange(slots);

                currentDay = currentDay.AddDays(1);
            }

            return result;
        }

        // Each product type will decide its own delivery start day
        protected abstract DateTime GetStartDay();

        // Each product type will decide which days are allowed for delivery
        protected abstract bool IsDeliveryDay(DateTime day);

        // This method creates 1-hour delivery slots for a single day
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
