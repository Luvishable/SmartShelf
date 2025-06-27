using FluentValidation;
using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Validators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(3, 50);

        RuleFor(c => c.Description)
            .MaximumLength(200).WithMessage("Description must be at most 200 characters.")
            .When(c => !string.IsNullOrWhiteSpace(c.Description));
    }
}