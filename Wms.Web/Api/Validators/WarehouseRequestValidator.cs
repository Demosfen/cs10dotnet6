using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class WarehouseRequestValidator : AbstractValidator<WarehouseRequest>
{
    public WarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name of the warehouse should not be null or empty.");

        RuleFor(x => x.Name.Length)
            .LessThanOrEqualTo(40)
            .WithMessage("Warehouse name sholuld be less than or equal to 40 characters");
    }
}