using Asp.Versioning;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        return Ok(await _dashboardService.GetSummaryAsync());
    }

    [HttpGet("recent-orders")]
    public async Task<IActionResult> RecentOrders()
    {
        return Ok(await _dashboardService.GetRecentOrdersAsync());
    }

    [HttpGet("top-selling-foods")]
    public async Task<IActionResult> GetTopSellingFoods()
    {
        return Ok(await _dashboardService.GetTopSellingFoodsAsync());
    }

    [HttpGet("revenue")]
    public async Task<IActionResult> Revenue()
    {
        return Ok(await _dashboardService.GetRevenueAsync());
    }

    [HttpGet("orders-by-status")]
    public async Task<IActionResult> GetOrdersByStatus()
    {
        return Ok(await _dashboardService.GetOrdersByStatusAsync());
    }

    [HttpGet("monthly-revenue")]
    public async Task<IActionResult> GetMonthlyRevenue()
    {
        return Ok(await _dashboardService.GetMonthlyRevenueAsync());
    }
}