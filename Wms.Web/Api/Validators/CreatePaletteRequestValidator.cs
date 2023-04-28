using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class CreatePaletteRequestValidator : AbstractValidator<CreatePaletteRequest>
{
    public CreatePaletteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull();
        
        RuleFor(x => x.WarehouseId)
            .NotEmpty().NotNull();
        
        RuleFor(x => x.PaletteRequest)
            .NotEmpty().NotNull();
    }
}