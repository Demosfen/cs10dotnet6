using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class UpdateBoxRequestValidation
{
    public sealed class CreateBoxRequestValidator : AbstractValidator<UpdateBoxRequest>
{
    public CreateBoxRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull()
            .WithMessage("Box should have Guid.");

        RuleFor(x => x.PaletteId)
            .NotEmpty().NotNull()
            .WithMessage("PaletteId should have Guid.");

        RuleFor(x => x.BoxRequest)
            .NotEmpty().NotNull()
            .WithMessage("Ensure that you typed box parameters (HxWxD) etc.");
    }
}
    
}