using FastFood.Core.DTOs.Admin;
using FastFood.Core.DTOs.Order;

public interface IAdminService
{
    Task<List<UserDto>> GetUsersAsync();

    Task DeleteUserAsync(string id);

    Task ChangeRoleAsync(string id, string role);

    Task<List<AdminOrderDto>> GetOrdersAsync();

    Task<AdminOrderDto?> GetOrderAsync(int id);

    Task UpdateOrderStatusAsync(int id, string status);

    Task<List<AdminOrderDto>> GetOrdersByStatusAsync(string status);

    Task<List<TopFoodDto>> GetTopSellingFoodsAsync();
}