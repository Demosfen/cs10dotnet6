using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/warehouses/palettes/boxes/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class BoxController : ControllerBase
{
    private readonly IPaletteService _paletteService;
    private readonly IBoxService _boxService;
    private readonly IMapper _mapper;


    public BoxController(IPaletteService paletteService, IBoxService boxService, IMapper mapper)
    {
        _paletteService = paletteService;
        _boxService = boxService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAll()
    {
        var boxDto = await _paletteService.GetAllAsync();
        var boxResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(boxDto);

        return Ok(boxResponse);
    }
    
    [HttpGet("{boxId}", Name = "GetBoxById")]
    public async Task<IActionResult> Get([FromRoute] Guid boxId)
    {
        var boxDto = await _boxService.GetByIdAsync(boxId);

        if (boxDto is null)
        {
            return NotFound();
        }

        var boxResponse = _mapper.Map<BoxResponse>(boxDto);
        return Ok(boxResponse);
    }

    // [HttpPost("{id:guid}", Name = "CreatePalette")]
    // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaletteDto))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(PaletteDto))]
    // public async Task<IActionResult> CreateAsync([FromRoute] Guid id, [FromBody] CreatePaletteRequest request)
    // {
    //     if (await _paletteService.GetByIdAsync(id) != null)
    //     {
    //         return Conflict("Palette with the same id already exist.");
    //     }
    //     
    //     var paletteDto = _mapper.Map<PaletteDto>(request);
    //
    //     paletteDto.Id = id;
    //
    //     await _paletteService.CreateAsync(paletteDto);
    //
    //     var response = _mapper.Map<PaletteResponse>(paletteDto);
    //     
    //     return Created("Palette created:", paletteDto);
    // }
    //
    // [HttpDelete("{id:guid}", Name = "DeletePalette")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult> DeleteAsync(Guid id)
    // {
    //     await _paletteService.DeleteAsync(id);
    //
    //     return Ok();
    // }
}