using CarInventory.Application.Commands.Car.Delete;
using FluentValidation;

namespace CarInventory.Application.Validators
{
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator()
        {
            RuleFor(x => x.CarId)
                .NotEmpty()
                .WithMessage("ID is required.");
        }
    }
}
