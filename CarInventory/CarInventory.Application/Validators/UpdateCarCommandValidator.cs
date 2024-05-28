using CarInventory.Application.Commands.Car.Update;
using FluentValidation;

namespace CarInventory.Application.Validators
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("ID is required.");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Make is required.");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required.");

            RuleFor(x => x.Year).
                InclusiveBetween(1886, DateTime.Now.Year)
                .WithMessage("Year must be between 1886 and current year.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
        }
    }
}
