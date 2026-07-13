using FastFood.Core.DTOs.Order;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.API.Controllers
{
    [ApiController]
    [Route("api/admin/orders")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            UpdateOrderStatusDto dto)
        {
            var result = await _orderService.UpdateOrderStatusAsync(
                id,
                dto.Status);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Order updated successfully."
            });
        }
    }
}