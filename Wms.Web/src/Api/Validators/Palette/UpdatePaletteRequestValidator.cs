using FluentValidation;
using Wms.Web.Api.Contracts;

namespace Wms.Web.Api.Validators.Palette;

public sealed class UpdatePaletteRequestValidator : AbstractValidator<UpdatePaletteRequest>
{
    public UpdatePaletteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Palette Id shouldn't be null or empty");
        
        RuleFor(x => x.WarehouseId)
            .NotEmpty()
            .WithMessage("WarehouseId for palette entity shouldn't be null or empty");
        
        RuleFor(x => x.PaletteRequest)
            .NotEmpty()
            .WithMessage("Check if PaletteRequest is empty");
    }
}