using FluentValidation;
using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Validators;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p => p.Barcode)
            .NotEmpty().WithMessage("Barcode is required.")
            .Length(3, 20);
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100);
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category is required.");
        RuleFor(p => p.SupplierId)
            .NotEmpty().WithMessage("Supplier is required.");
        RuleFor(p => p.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(p => p.Unit)
            .Equal("kg").WithMessage("Only 'kg' is accepted as unit.");

        RuleFor(p => p.PurchasePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Purchase price must be non-negative.");

        RuleFor(p => p.EntryDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Entry date cannot be in the future.");
    }
}