using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class UpdateWarehouseRequestValidator: AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull()
            .WithMessage("Name of the warehouse should not be null or empty.");

        RuleFor(x => x.Name.Length)
            .LessThanOrEqualTo(15)
            .WithMessage("Name of the warehouse should not be greater than 15 characters.");
    }
}