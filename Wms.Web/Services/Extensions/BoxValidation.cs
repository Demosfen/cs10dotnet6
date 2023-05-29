using Wms.Web.Common.Exceptions;
using Wms.Web.Services.Dto;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Extensions;

internal sealed class BoxValidations
{
    private const int ExpiryDays = 100;

    public static void BoxSizeValidation(Palette paletteDto, BoxDto boxDto)
    {
        if (boxDto.Width > paletteDto.Width 
            | boxDto.Height > paletteDto.Height 
            | boxDto.Depth > paletteDto.Depth)
        {
            throw new UnitOversizeException(boxDto.Id);
        }
    }

    public static void BoxExpiryValidation(Palette paletteDto, BoxDto boxDto)
    {
        if (boxDto.ProductionDate != null)
        {
            boxDto.ExpiryDate ??= boxDto.ProductionDate.Value.AddDays(ExpiryDays);
        }
        else
        {
            if (boxDto.ExpiryDate == null)
            {
                throw new EntityExpiryDateException(boxDto.Id);
            }
        }
        
        if (boxDto.ExpiryDate <= boxDto.ProductionDate)
        {
            throw new EntityExpiryDateException(boxDto.Id);
        }
    }
}