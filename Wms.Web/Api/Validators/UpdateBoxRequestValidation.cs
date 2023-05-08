using FluentValidation;
using Wms.Web.Api.Contracts;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class UpdateBoxRequestValidation
{
    public sealed class CreateBoxRequestValidator : AbstractValidator<UpdateBoxRequest>
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
    
}