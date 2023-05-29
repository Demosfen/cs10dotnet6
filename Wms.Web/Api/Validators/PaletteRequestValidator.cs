using FluentValidation;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class PaletteRequestValidator : AbstractValidator<PaletteRequest>
{
    public PaletteRequestValidator()
    {
        RuleFor(x => x.Width)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Palette width should not be zero or negative.")
            .LessThanOrEqualTo(200)
            .WithMessage("Palette width too big");

        RuleFor(x => x.Height)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Palette height should not be zero or negative.")
            .LessThanOrEqualTo(200)
            .WithMessage("Palette height too big");
        
        RuleFor(x => x.Depth)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Palette depth should not be zero or negative.")
            .LessThanOrEqualTo(200)
            .WithMessage("Palette depth too big");
    }
}