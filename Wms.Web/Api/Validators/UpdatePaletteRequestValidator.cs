using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class UpdatePaletteRequestValidator : AbstractValidator<UpdatePaletteRequest>
{
    public UpdatePaletteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull();
        
        RuleFor(x => x.WarehouseId)
            .NotEmpty().NotNull();
        RuleFor(x => x.PaletteRequest)
            .NotEmpty().NotNull();
    }
}