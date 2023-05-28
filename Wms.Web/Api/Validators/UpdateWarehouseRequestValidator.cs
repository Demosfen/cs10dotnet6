using FluentValidation;
using Wms.Web.Api.Contracts;

namespace Wms.Web.Api.Validators;

public sealed class UpdateWarehouseRequestValidator: AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Warehouse Id shouldn't be empty");
    }
}