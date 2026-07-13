using AutoMapper;
using FastFood.Core.DTOs.Cart;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;

namespace FastFood.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartDto?> GetCartAsync(string userId)
        {
            var cart = await _unitOfWork.Carts
                .GetByIdWithIncludeAsync(
                    c => c.UserId == userId,
                    c => c.CartItems);

            if (cart == null)
                return null;

            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = new List<CartItemDto>()
            };

            foreach (var item in cart.CartItems)
            {
                var food = await _unitOfWork.FoodItems.GetByIdAsync(item.FoodItemId);

                cartDto.Items.Add(new CartItemDto
                {
                    Id = item.Id,
                    FoodItemId = item.FoodItemId,
                    FoodName = food?.Name ?? "",
                    Price = food?.Price ?? 0,
                    Quantity = item.Quantity
                });
            }

            return cartDto;
        }

        public async Task AddToCartAsync(
            string userId,
            AddToCartDto dto)
        {
            var cart = await _unitOfWork.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
            }

            var cartItem = await _unitOfWork.CartItems
                .FirstOrDefaultAsync(c =>
                    c.CartId == cart.Id &&
                    c.FoodItemId == dto.FoodItemId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    FoodItemId = dto.FoodItemId,
                    Quantity = dto.Quantity
                };

                await _unitOfWork.CartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += dto.Quantity;

                _unitOfWork.CartItems.Update(cartItem);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateQuantityAsync(
            string userId,
            int foodItemId,
            int quantity)
        {
            var cart = await _unitOfWork.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            var item = await _unitOfWork.CartItems
                .FirstOrDefaultAsync(c =>
                    c.CartId == cart.Id &&
                    c.FoodItemId == foodItemId);

            if (item == null)
                return false;

            item.Quantity = quantity;

            _unitOfWork.CartItems.Update(item);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> RemoveItemAsync(
            string userId,
            int foodItemId)
        {
            var cart = await _unitOfWork.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            var item = await _unitOfWork.CartItems
                .FirstOrDefaultAsync(c =>
                    c.CartId == cart.Id &&
                    c.FoodItemId == foodItemId);

            if (item == null)
                return false;

            _unitOfWork.CartItems.Delete(item);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await _unitOfWork.Carts
                .GetByIdWithIncludeAsync(
                    c => c.UserId == userId,
                    c => c.CartItems);

            if (cart == null)
                return false;

            foreach (var item in cart.CartItems.ToList())
            {
                _unitOfWork.CartItems.Delete(item);
            }

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}