using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Wms.Web.Services.Abstract;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class PaletteController : ControllerBase
{
    private readonly IPaletteService _paletteService;
    private readonly IBoxService _boxService;
    private readonly IMapper _mapper;

    public PaletteController(
        IPaletteService paletteService, 
        IMapper mapper, IBoxService boxService)
    {
        _paletteService = paletteService;
        _mapper = mapper;
        _boxService = boxService;
    }
    
    [HttpGet("warehouses/{warehouseId:guid}/palettes/", Name = "GetNotDeletedPalettes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllNotDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 10)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("warehouses/{warehouseId:guid}/palettes/archive/", Name = "GetDeletedPalettes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 10)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit, true);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("palettes/{paletteId}", Name = "GetPaletteById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaletteResponse>> GetByIdAsync(
        [FromRoute] Guid paletteId, 
        int boxListOffset = 0, int boxListSize = 10, 
        CancellationToken cancellationToken = default)
    {
        var paletteDto = await _paletteService.GetByIdAsync(paletteId, cancellationToken);
        
        var boxDto = await _boxService.GetAllAsync(
            paletteId,
            boxListOffset, boxListSize, false,
            cancellationToken);
        
        paletteDto?.Boxes.AddRange(boxDto);
        
        return Ok(_mapper.Map<PaletteResponse>(paletteDto));
    }

    [HttpPost("warehouses/{warehouseId:guid}", Name = "CreatePalette")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HttpResponseMessage>> CreateAsync(
        [FromRoute][Required] Guid warehouseId, 
        [FromQuery][Required] Guid paletteId,
        [FromBody] PaletteRequest request,
        CancellationToken cancellationToken = default)
    {
        var createRequest = new CreatePaletteRequest
        {
            Id = paletteId,
            WarehouseId = warehouseId,
            PaletteRequest = request
        };
        
        var paletteDto = _mapper.Map<PaletteDto>(createRequest);

        await _paletteService.CreateAsync(paletteDto, cancellationToken);
        
        var locationUri = Url.Link("GetPaletteById", new { paletteId });
        
        return Created(locationUri ?? throw new InvalidOperationException(),  
            _mapper.Map<PaletteResponse>(paletteDto));
    }
    
    [HttpPut("palettes/{paletteId:guid}", Name = "UpdatePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaletteResponse>> UpdateAsync(
        [FromRoute] Guid paletteId,
        [FromQuery] Guid warehouseId,
        [FromBody] PaletteRequest request,
        CancellationToken cancellationToken = default)
    {
        var updateRequest = new UpdatePaletteRequest
        {
            Id = paletteId,
            WarehouseId = warehouseId,
            PaletteRequest = request
        };
        
        var paletteDto = _mapper.Map<PaletteDto>(updateRequest);

        await _paletteService.UpdateAsync(paletteDto, cancellationToken);

        return Ok(_mapper.Map<PaletteResponse>(paletteDto));
    }
    
    [HttpDelete("palettes/{paletteId}", Name = "DeletePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid paletteId, CancellationToken cancellationToken = default)
    {
        await _paletteService.DeleteAsync(paletteId, cancellationToken);

        return Ok("Palette deleted");
    }
}