using FastFood.Core.DTOs.Cart;

namespace FastFood.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(string userId);

        Task AddToCartAsync(
            string userId,
            AddToCartDto dto);

        Task<bool> RemoveItemAsync(
            string userId,
            int foodItemId);

        Task<bool> UpdateQuantityAsync(
            string userId,
            int foodItemId,
            int quantity);

        Task<bool> ClearCartAsync(
            string userId);
    }
}