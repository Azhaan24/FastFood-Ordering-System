using FastFood.Core.DTOs.Admin;
using FastFood.Core.DTOs.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/v{version:apiVersion}/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        return Ok(await _service.GetUsersAsync());
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteUserAsync(id);

        return NoContent();
    }

    [HttpPut("users/{id}/role")]
    public async Task<IActionResult> ChangeRole(string id, string role)
    {
        await _service.ChangeRoleAsync(id, role);

        return Ok();
    }

    [HttpGet("users/orders")]
    public async Task<IActionResult> Orders()
    {
        return Ok(await _service.GetOrdersAsync());
    }

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> Order(int id)
    {
        var order = await _service.GetOrderAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("orders/{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status)
    {
        await _service.UpdateOrderStatusAsync(id, status);

        return Ok();
    }

    [HttpGet("orders/status/{status}")]
    public async Task<IActionResult> ByStatus(string status)
    {
        return Ok(await _service.GetOrdersByStatusAsync(status));
    }

    [HttpGet("top-foods")]
    public async Task<IActionResult> TopFoods()
    {
        return Ok(await _service.GetTopSellingFoodsAsync());
    }
}