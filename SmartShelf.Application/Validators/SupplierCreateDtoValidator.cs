using FluentValidation;
using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Validators;

public class SupplierCreateDtoValidator : AbstractValidator<SupplierCreateDto>
{
    public SupplierCreateDtoValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Supplier name is required.")
            .Length(3, 100);

        RuleFor(s => s.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required.")
            .Length(10, 15).WithMessage("Tax number must be between 10 and 15 digits.");

        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(s => s.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

        RuleFor(s => s.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(250);
    }
}