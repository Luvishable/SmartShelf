using FluentValidation;
using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Validators;

public class ShelfCreateDtoValidator : AbstractValidator<ShelfCreateDto>
{
    public ShelfCreateDtoValidator()
    {
        RuleFor(s => s.Code)
            .NotEmpty().WithMessage("Shelf code is required.")
            .Length(3, 50);

        RuleFor(s => s.MaxCapacity)
            .GreaterThan(0).WithMessage("Maximum capacity must be greater than 0.");
    }
}