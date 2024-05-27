using CarInventory.Application.Commands.Car.Create;
using CarInventory.Application.Dtos;
using FluentValidation;

namespace CarInventory.Application.Validators
{
    public class CarValidator : AbstractValidator<CreateCarCommand>
    {
        public CarValidator()
        {
            RuleFor(car => car.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .MaximumLength(50).WithMessage("Brand must not exceed 50 characters.");

            RuleFor(car => car.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

            RuleFor(car => car.Year)
                .InclusiveBetween(1886, DateTime.Now.Year).WithMessage("Year must be a valid year.");

            RuleFor(car => car.VIN)
                .NotEmpty().WithMessage("VIN is required.")
                .Length(17).WithMessage("VIN must be exactly 17 characters.");

            RuleFor(car => car.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
