using FluentValidation;
using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Validators;

public class AddProductToShelfDtoValidator : AbstractValidator<AddProductToShelfDto>
{
    public AddProductToShelfDtoValidator()
    {
        RuleFor(x => x.ShelfId)
            .NotEmpty().WithMessage("ShelfId is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}