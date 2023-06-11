using Microsoft.Extensions.Logging;
using Serilog;
using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.Business.Extensions;

internal static class BoxValidationExtensions
{
    private const int ExpiryDays = 100;

    public static void Validate(this BoxDto boxDto, Palette palette)
    {
        BoxSizeValidation(boxDto, palette);
        BoxExpiryValidation(boxDto, palette);
    }

    private static void BoxSizeValidation(BoxDto boxDto, Palette palette)
    {
        if (boxDto.Width > palette.Width 
            | boxDto.Height > palette.Height 
            | boxDto.Depth > palette.Depth)
        {
            Log.Logger.Error("Box oversize. \n {Box} \n {Palette}",
                boxDto.ToString(),
                palette.ToString());

            throw new UnitOversizeException(boxDto.Id);
        }
    }

    private static void BoxExpiryValidation(BoxDto boxDto, Palette palette)
    {
        if (boxDto.ProductionDate != null)
        {
            boxDto.ExpiryDate ??= boxDto.ProductionDate.Value.AddDays(ExpiryDays);
        }
        else
        {
            if (boxDto.ExpiryDate == null)
            {
                Log.Logger.Error("Box expiry and production is null");
                
                throw new EntityExpiryDateException(boxDto.Id);
            }
        }
        
        if (boxDto.ExpiryDate <= boxDto.ProductionDate)
        {
            Log.Logger.Error("Box Expiry lower than box Production date: " +
                             "\n {Expiry} \n {Production}",
                boxDto.ExpiryDate,
                boxDto.ProductionDate);
            
            throw new EntityExpiryDateException(boxDto.Id);
        }
    }
}