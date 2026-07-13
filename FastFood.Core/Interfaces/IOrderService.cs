using FastFood.Core.DTOs.Order;

namespace FastFood.Core.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(string userId);

    Task<OrderDto?> GetOrderAsync(int orderId);

    Task<OrderDto> CreateOrderAsync(string userId, CreateOrderDto dto);

    Task<bool> CancelOrderAsync(int orderId);

    // Admin

    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
}