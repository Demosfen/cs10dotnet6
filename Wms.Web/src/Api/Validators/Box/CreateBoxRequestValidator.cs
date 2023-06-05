using FluentValidation;
using Wms.Web.Api.Contracts;

namespace Wms.Web.Api.Validators.Box;

internal sealed class CreateBoxRequestValidator : AbstractValidator<CreateBoxRequest>
{
    public CreateBoxRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Box should have Guid.");

        RuleFor(x => x.PaletteId)
            .NotEmpty()
            .WithMessage("PaletteId should have Guid.");

        RuleFor(x => x.BoxRequest)
            .NotEmpty()
            .WithMessage("Ensure that you typed box parameters (HxWxD) etc.");
    }
}