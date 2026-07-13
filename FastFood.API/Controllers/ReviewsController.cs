using FastFood.Core.DTOs.Review;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;

namespace FastFood.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{foodItemId}")]
    public async Task<IActionResult> Get(int foodItemId)
    {
        return Ok(await _reviewService.GetReviewsAsync(foodItemId));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add(CreateReviewDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        return Ok(await _reviewService.AddReviewAsync(userId, dto));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reviewService.DeleteReviewAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}