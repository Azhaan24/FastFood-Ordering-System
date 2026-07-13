using FastFood.Core.DTOs.Dashboard;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        return new DashboardSummaryDto
        {
            TotalUsers = await _userManager.Users.CountAsync(),

            TotalOrders = await _context.Orders.CountAsync(),

            TotalFoodItems = await _context.FoodItems.CountAsync(),

            TotalRevenue = await _context.Orders
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0
        };
    }

    public async Task<List<RecentOrderDto>> GetRecentOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.OrderDate)
            .Take(10)
            .Select(o => new RecentOrderDto
            {
                OrderId = o.Id,
                Customer = o.User.FullName,
                Amount = o.TotalAmount,
                Status = o.Status,
                OrderDate = o.OrderDate
            })
            .ToListAsync();
    }

    public async Task<List<TopSellingFoodDto>> GetTopSellingFoodsAsync()
    {
        return await _context.OrderItems
            .Include(o => o.FoodItem)
            .GroupBy(o => o.FoodItem.Name)
            .Select(g => new TopSellingFoodDto
            {
                FoodName = g.Key,
                QuantitySold = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.QuantitySold)
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<RevenueDto>> GetRevenueAsync()
    {
        return await _context.Orders
            .GroupBy(x => new
            {
                x.OrderDate.Year,
                x.OrderDate.Month
            })
            .Select(g => new RevenueDto
            {
                Month = $"{g.Key.Month}/{g.Key.Year}",
                Revenue = g.Sum(x => x.TotalAmount)
            })
            .OrderBy(x => x.Month)
            .ToListAsync();
    }

    public async Task<List<OrderStatusDto>> GetOrdersByStatusAsync()
    {
        return await _context.Orders
            .GroupBy(o => o.Status)
            .Select(g => new OrderStatusDto
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
    }

    public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync()
    {
        return await _context.Orders
            .Where(o => o.Status == "Delivered")
            .GroupBy(o => new
            {
                o.OrderDate.Year,
                o.OrderDate.Month
            })
            .Select(g => new MonthlyRevenueDto
            {
                Month = $"{g.Key.Month}/{g.Key.Year}",
                Revenue = g.Sum(x => x.TotalAmount)
            })
            .OrderBy(x => x.Month)
            .ToListAsync();
    }
}