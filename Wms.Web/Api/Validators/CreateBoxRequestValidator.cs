using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class CreateBoxRequestValidator : AbstractValidator<CreateBoxRequest>
{
    public CreateBoxRequestValidator()
    {
        RuleFor(x => x.Width)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Box width should not be zero or negative.");

        RuleFor(x => x.Height)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Box height should not be zero or negative.");
        
        RuleFor(x => x.Depth)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Box depth should not be zero or negative.");
        
        RuleFor(x => x.Weight)
            .NotEmpty().NotNull()
            .GreaterThan(0)
            .WithMessage("Box weight should not be zero or negative.");
    }
}