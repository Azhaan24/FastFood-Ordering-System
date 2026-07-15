using Asp.Versioning;
using FastFood.Core.DTOs.Common;
using FastFood.Core.DTOs.Food;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace FastFood.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;

        public FoodItemsController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        // GET: api/FoodItems
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetAll([FromQuery] FoodQueryParameters query)
        {
            var foods = await _foodItemService.GetAllAsync(query);

            return Ok(foods);
        }

        // GET: api/FoodItems/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var foodItem = await _foodItemService.GetByIdAsync(id);

            if (foodItem == null)
            {
                return NotFound(new
                {
                    Message = "Food item not found."
                });
            }

            return Ok(foodItem);
        }

        // POST: api/FoodItems
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateFoodItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var food = await _foodItemService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = food.Id },
                food);
        }

        // PUT: api/FoodItems/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id,[FromForm] UpdateFoodItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _foodItemService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/FoodItems/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _foodItemService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Food item not found."
                });
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            return Ok(await _foodItemService.SearchAsync(keyword));
        }
    }
}