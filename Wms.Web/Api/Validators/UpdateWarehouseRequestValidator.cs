using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class UpdateWarehouseRequestValidator: AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull();
    }
}