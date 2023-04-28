using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class CreateWarehouseRequestValidator : AbstractValidator<CreateWarehouseRequest>
{
    public CreateWarehouseRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull()
            .WithMessage("Name of the warehouse should not be null or empty.");
    }
}