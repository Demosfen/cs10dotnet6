using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class CreateWarehouseRequestValidator : AbstractValidator<CreateWarehouseRequest>
{
    public CreateWarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull()
            .WithMessage("Name of the warehouse should not be null or empty.");

        RuleFor(x => x.Name.Length)
            .LessThanOrEqualTo(10)
            .WithMessage("Name of the warehouse should not be greater than 10 characters.");
    }
}