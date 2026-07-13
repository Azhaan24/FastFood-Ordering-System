using FastFood.Core.DTOs.Food;
using FluentValidation;

namespace FastFood.API.Validators;

public class CreateFoodItemValidator : AbstractValidator<CreateFoodItemDto>
{
    public CreateFoodItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.Image)
            .Must(file => file == null || file.Length > 0)
            .WithMessage("Image file must not be empty.");
    }
}