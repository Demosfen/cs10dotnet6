using FluentValidation;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public sealed class BoxRequestValidator : AbstractValidator<BoxRequest>
{
    public BoxRequestValidator()
    {
        RuleFor(x => x.Width)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Box width should not be zero or negative.")
            .LessThanOrEqualTo(100)
            .WithMessage("Box width too big");

        RuleFor(x => x.Height)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Box height should not be zero or negative.")
            .LessThanOrEqualTo(100)
            .WithMessage("Box height too big");
        
        RuleFor(x => x.Depth)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Box depth should not be zero or negative.")
            .LessThanOrEqualTo(100)
            .WithMessage("Box depth too big");
        
        RuleFor(x => x.Weight)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Box weight should not be zero or negative.")
            .LessThanOrEqualTo(200)
            .WithMessage("Box weight too big");
    }
}