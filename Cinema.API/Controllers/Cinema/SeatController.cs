using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.DTO.Seat;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema;

[ApiController]
[Route("[controller]")]
public class SeatController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;

    public SeatController(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeatDto>>> GetAll()
    {
        return Ok(await _seatRepository.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SeatDto>> GetById(Guid id)
    {
        var seat = await _seatRepository.GetByIdAsync(id);
        return seat is null ? NotFound() : Ok(seat);
    }

    [HttpGet("by-auditorium/{auditoriumId:guid}")]
    public async Task<ActionResult<IEnumerable<SeatDto>>> GetByAuditoriumId(Guid auditoriumId)
    {
        return Ok(await _seatRepository.GetByAuditoriumIdAsync(auditoriumId));
    }

    /*[HttpPost]
    public async Task<ActionResult<SeatDto>> Create([FromBody] CreateSeatDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _seatRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }*/

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSeatDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _seatRepository.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{auditoriumId:guid}")]
    public async Task<IActionResult> Delete(Guid auditoriumId)
    {
        var deleted = await _seatRepository.DeleteAsync(auditoriumId);
        return deleted ? NoContent() : NotFound();
    }
}