﻿using DeliverySystem.Application.DTOS;
using DeliverySystem.BLL.Interfaces;
using DeliverySystemTask.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystemTask.Controllers
{
    [Route("api/[controller]")] // Routing =>  Post : api/Delivery/slots
    [ApiController]
    public class DeliveryController(IDeliveryService deliveryService) : ControllerBase
    {

        private readonly IDeliveryService _deliveryService = deliveryService;

        [HttpPost("slots")]
        public ActionResult<List<DeliveryDto>> GetDeliverySlots([FromBody] DeliveryRequestDto request)
        {
            if (request == null || request.ProductDto == null || request.ProductDto.Count == 0)
            {
                return BadRequest(" Invalid Request Data .....");
            }

            var slots = _deliveryService.GetDeliverySlots(request.ProductDto, request.Time);

            return Ok(slots);
        }
   
    }
}
