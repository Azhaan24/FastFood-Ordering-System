using FastFood.Core.DTOs.Cart;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastFood.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private string? UserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            if (UserId == null)
                return Unauthorized();

            var cart = await _cartService.GetCartAsync(UserId);

            if (cart == null)
                return NotFound("Cart is empty.");

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            if (UserId == null)
                return Unauthorized();

            await _cartService.AddToCartAsync(UserId, dto);

            return Ok(new
            {
                Message = "Item added to cart."
            });
        }

        [HttpPut("{foodItemId}")]
        public async Task<IActionResult> UpdateQuantity(
            int foodItemId,
            UpdateCartQuantityDto dto)
        {
            if (UserId == null)
                return Unauthorized();

            var result = await _cartService.UpdateQuantityAsync(
                UserId,
                foodItemId,
                dto.Quantity);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Quantity updated."
            });
        }

        [HttpDelete("{foodItemId}")]
        public async Task<IActionResult> RemoveItem(
            int foodItemId)
        {
            if (UserId == null)
                return Unauthorized();

            var result = await _cartService.RemoveItemAsync(
                UserId,
                foodItemId);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Item removed."
            });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            if (UserId == null)
                return Unauthorized();

            var result = await _cartService.ClearCartAsync(UserId);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Cart cleared."
            });
        }
    }
}