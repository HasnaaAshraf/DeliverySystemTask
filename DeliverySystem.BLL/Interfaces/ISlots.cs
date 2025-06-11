using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystemTask.DTOS;

namespace DeliverySystem.Application.Interfaces
{
    public interface ISlots
    {
        List<DeliveryDto> GetSlots();
    }
}
