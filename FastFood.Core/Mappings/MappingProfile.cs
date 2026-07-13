using AutoMapper;
using FastFood.Core.DTOs.Cart;
using FastFood.Core.DTOs.Category;
using FastFood.Core.DTOs.Food;
using FastFood.Core.DTOs.Order;
using FastFood.Core.DTOs.Payment;
using FastFood.Core.DTOs.Review;
using FastFood.Core.Entities;

namespace FastFood.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<FoodItem, FoodItemDto>().ReverseMap();

            CreateMap<CreateFoodItemDto, FoodItem>();

            CreateMap<UpdateFoodItemDto, FoodItem>();

            CreateMap<Cart, CartDto>();

            CreateMap<CartItem, CartItemDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<CreateOrderDto, Order>();

            CreateMap<Review, ReviewDto>();

            CreateMap<Review, ReviewDto>().ReverseMap();

            CreateMap<CreateReviewDto, Review>();

            CreateMap<UpdateReviewDto, Review>();

            CreateMap<Payment, PaymentResponseDto>();
        }
    }
}