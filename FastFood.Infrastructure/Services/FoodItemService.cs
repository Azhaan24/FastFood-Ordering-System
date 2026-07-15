using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFood.Core.DTOs.Common;
using FastFood.Core.DTOs.Food;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        public FoodItemService(IUnitOfWork unitOfWork,IMapper mapper,IFileService fileService,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _context = context;
        }

        public async Task<PagedResponse<FoodItemDto>> GetAllAsync(FoodQueryParameters query)
        {
            var foodItems = await _unitOfWork.FoodItems.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                foodItems = foodItems.Where(x =>
                    x.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    (x.Description != null &&
                     x.Description.Contains(query.Search, StringComparison.OrdinalIgnoreCase)));
            }

            if (query.CategoryId.HasValue)
            {
                foodItems = foodItems.Where(x => x.CategoryId == query.CategoryId.Value);
            }

            if (query.IsAvailable.HasValue)
            {
                foodItems = foodItems.Where(x => x.IsAvailable == query.IsAvailable.Value);
            }

            var totalRecords = foodItems.Count();

            var data = foodItems
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            return new PagedResponse<FoodItemDto>
            {
                Items = _mapper.Map<IEnumerable<FoodItemDto>>(data),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / query.PageSize)
            };
        }

        public async Task<FoodItemDto?> GetByIdAsync(int id)
        {
            var foodItem = await _unitOfWork.FoodItems
                .GetByIdWithIncludeAsync(
                    x => x.Id == id,
                    x => x.Category);

            if (foodItem == null)
                return null;

            return _mapper.Map<FoodItemDto>(foodItem);
        }

        public async Task<FoodItemDto> CreateAsync(CreateFoodItemDto dto)
        {
            var foodItem = _mapper.Map<FoodItem>(dto);

            if (dto.Image != null)
            {
                foodItem.ImageUrl =
                    await _fileService.UploadFoodImageAsync(dto.Image);
            }

            await _unitOfWork.FoodItems.AddAsync(foodItem);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<FoodItemDto>(foodItem);
        }

        public async Task<bool> UpdateAsync(int id, UpdateFoodItemDto dto)
        {
            var foodItem = await _unitOfWork.FoodItems.GetByIdAsync(id);

            if (foodItem == null)
                return false;

            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(foodItem.ImageUrl))
                {
                    _fileService.DeleteFile(foodItem.ImageUrl);
                }

                foodItem.ImageUrl =
                    await _fileService.UploadFoodImageAsync(dto.Image);
            }

            foodItem.Name = dto.Name;
            foodItem.Description = dto.Description;
            foodItem.Price = dto.Price;
            foodItem.CategoryId = dto.CategoryId;
            foodItem.IsAvailable = dto.IsAvailable;

            _unitOfWork.FoodItems.Update(foodItem);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var foodItem = await _unitOfWork.FoodItems.GetByIdAsync(id);

            if (foodItem == null)
                return false;

            if (!string.IsNullOrEmpty(foodItem.ImageUrl))
            {
                _fileService.DeleteFile(foodItem.ImageUrl);
            }

            _unitOfWork.FoodItems.Delete(foodItem);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<FoodItemDto>> GetByCategoryAsync(int categoryId)
        {
            var foodItems = await _unitOfWork.FoodItems.FindAsync(
                x => x.CategoryId == categoryId);

            return _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);
        }

        public async Task<List<FoodItemDto>> SearchAsync(string keyword)
        {
            return await _context.FoodItems

                .Where(x =>
                    x.Name.Contains(keyword) ||
                    x.Description.Contains(keyword))

                .ProjectTo<FoodItemDto>(_mapper.ConfigurationProvider)

                .ToListAsync();
        }

        public async Task<IEnumerable<FoodItemDto>> GetAvailableAsync()
        {
            var foodItems = await _unitOfWork.FoodItems.FindAsync(
                x => x.IsAvailable);

            return _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);
        }

        public async Task<PagedResponse<FoodItemDto>> GetPagedAsync(int page,int pageSize)
        {
            var query = _context.FoodItems.AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<FoodItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResponse<FoodItemDto>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = total,
                Items = items
            };
        }
    }
}