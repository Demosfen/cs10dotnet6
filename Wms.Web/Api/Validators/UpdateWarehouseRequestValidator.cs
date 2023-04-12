using FluentValidation;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Validators;

public class UpdateWarehouseRequestValidator: AbstractValidator<CreateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        Include(new CreateWarehouseRequestValidator());
    }
}