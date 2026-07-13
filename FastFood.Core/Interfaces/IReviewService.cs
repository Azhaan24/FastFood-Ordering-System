using FastFood.Core.DTOs.Review;

namespace FastFood.Core.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetReviewsAsync(int foodItemId);

    Task<ReviewDto> AddReviewAsync(
        string userId,
        CreateReviewDto dto);

    Task<bool> DeleteReviewAsync(int reviewId);
}