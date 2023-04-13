using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class CreatePaletteRequestValidator : AbstractValidator<CreatePaletteRequest>
{
    public CreatePaletteRequestValidator()
    {
        RuleFor(x => x.Width)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Palette width should not be zero or negative.");

        RuleFor(x => x.Height)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Palette height should not be zero or negative.");
        
        RuleFor(x => x.Depth)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Palette depth should not be zero or negative.");
    }
}