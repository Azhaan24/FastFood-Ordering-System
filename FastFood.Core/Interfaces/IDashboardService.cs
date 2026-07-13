using FastFood.Core.DTOs.Dashboard;

namespace FastFood.Core.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();

    Task<List<RecentOrderDto>> GetRecentOrdersAsync();

    Task<List<TopSellingFoodDto>> GetTopSellingFoodsAsync();

    Task<List<RevenueDto>> GetRevenueAsync();

    Task<List<OrderStatusDto>> GetOrdersByStatusAsync();

    Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync();
}