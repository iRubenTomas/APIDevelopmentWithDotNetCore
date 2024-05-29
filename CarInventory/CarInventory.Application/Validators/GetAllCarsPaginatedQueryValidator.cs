using CarInventory.Application.Queries.Car.Paginated;
using FluentValidation;


namespace CarInventory.Application.Validators
{
    public class GetAllCarsPaginatedQueryValidator : AbstractValidator<GetAllCarsPaginatedQuery>
    {
        public GetAllCarsPaginatedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
        }
    }
}
