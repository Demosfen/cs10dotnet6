using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class BoxController : ControllerBase
{
    private readonly IBoxService _boxService;
    private readonly IMapper _mapper;

    public BoxController(IBoxService boxService, 
        IMapper mapper)
    {
        _boxService = boxService;
        _mapper = mapper;
    }
    
    [HttpGet("palettes/{paletteId}/boxes", Name = "GetAllBoxes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<BoxResponse>>> GetNotDeletedAsync(
        [FromRoute] Guid paletteId,
        int offset = 0, 
        int size = 10,
        CancellationToken cancellationToken = default)
    {
        var boxesDto = await _boxService
            .GetAllAsync(paletteId, offset, size, false, cancellationToken);
        
        var boxResponse = _mapper.Map<IReadOnlyCollection<BoxResponse>>(boxesDto);

        return Ok(boxResponse);
    }

    [HttpGet("palettes/{paletteId}/boxes/archive", Name = "GetDeletedBoxes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<BoxResponse>>> GetDeletedAsync(
        [FromRoute] Guid paletteId,
        int offset = 0,
        int size = 10,
        CancellationToken cancellationToken = default)
    {
        var boxDto = await _boxService
            .GetAllAsync(paletteId, offset, size, true, cancellationToken);
       
        var boxResponse = _mapper.Map<IReadOnlyCollection<BoxResponse>>(boxDto);
    
        return Ok(boxResponse);
    }
    
    [HttpGet("boxes/{boxId}", Name = "GetBoxById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoxResponse>> GetByIdAsync(
        [FromRoute] Guid boxId, 
        CancellationToken cancellationToken = default)
    {
        var boxDto = await _boxService.GetByIdAsync(boxId, cancellationToken);

        return Ok(_mapper.Map<BoxResponse>(boxDto));
    }

    [HttpPost("palettes/{paletteId:guid}", Name = "CreateBox")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BoxResponse>> CreateAsync(
        [FromRoute][Required] Guid paletteId,
        [FromQuery][Required] Guid boxId,
        [FromBody] BoxRequest request,
        CancellationToken cancellationToken = default)
    {
        var createBox = new CreateBoxRequest
        {
            Id = boxId,
            PaletteId = paletteId,
            BoxRequest = request
        };

        var boxDto = _mapper.Map<BoxDto>(createBox);
    
        await _boxService.CreateAsync(boxDto, cancellationToken);
        
        var locationUri = Url.Link("GetBoxById", new { boxId });
        
        return Created(locationUri ?? throw new InvalidOperationException(),  
            _mapper.Map<BoxResponse>(boxDto));
    }

    [HttpPut("boxes/{boxId:guid}", Name = "UpdateBox")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoxResponse>> UpdateAsync(
        [FromRoute] Guid boxId,
        [FromQuery] Guid paletteId,
        [FromBody] BoxRequest request,
        CancellationToken cancellationToken = default)
    {
        var updateRequest = new UpdateBoxRequest
        {
            Id = boxId,
            PaletteId = paletteId,
            BoxRequest = request
        };

        var boxDto = _mapper.Map<BoxDto>(updateRequest);

        await _boxService.UpdateAsync(boxDto, cancellationToken);

        return Ok(_mapper.Map<BoxResponse>(boxDto));
    }

    [HttpDelete("boxes/{boxId:guid}", Name = "DeleteBox")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid boxId)
    {
        await _boxService.DeleteAsync(boxId);
    
        return NoContent();
    }
}