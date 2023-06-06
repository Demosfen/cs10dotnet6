using FluentValidation;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Validators.Warehouse;

public sealed class WarehouseRequestValidator : AbstractValidator<WarehouseRequest>
{
    public WarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name of the warehouse should not be null or empty.");

        RuleFor(x => x.Name.Length)
            .LessThanOrEqualTo(40)
            .WithMessage("Warehouse name should be less than or equal to 40 characters");
    }
}