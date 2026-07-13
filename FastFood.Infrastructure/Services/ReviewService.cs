using AutoMapper;
using FastFood.Core.DTOs.Review;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;

namespace FastFood.Infrastructure.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(int foodItemId)
    {
        var reviews = await _unitOfWork.Reviews.FindAsync(
            r => r.FoodItemId == foodItemId);

        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> AddReviewAsync(
        string userId,
        CreateReviewDto dto)
    {
        var review = new Review
        {
            UserId = userId,
            FoodItemId = dto.FoodItemId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Reviews.AddAsync(review);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<bool> DeleteReviewAsync(int reviewId)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);

        if (review == null)
            return false;

        _unitOfWork.Reviews.Delete(review);

        await _unitOfWork.CompleteAsync();

        return true;
    }
}