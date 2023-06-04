using FluentValidation;
using Wms.Web.Api.Contracts;

namespace Wms.Web.Api.Validators.Warehouse;

public sealed class CreateWarehouseRequestValidator : AbstractValidator<CreateWarehouseRequest>
{
    public CreateWarehouseRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("ID of the warehouse should not be null.");
    }
}