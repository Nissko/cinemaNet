using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.DTO.Auditorium;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema;

[ApiController]
[Route("[controller]")]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumRepository _auditoriumRepository;

    public AuditoriumController(IAuditoriumRepository adAuditoriumRepository)
    {
        _auditoriumRepository =
            adAuditoriumRepository ?? throw new ArgumentNullException(nameof(adAuditoriumRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetAll()
    {
        return Ok(await _auditoriumRepository.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuditoriumDto>> GetById(Guid id)
    {
        var auditorium = await _auditoriumRepository.GetByIdAsync(id);
        return auditorium is null ? NotFound() : Ok(auditorium);
    }

    [HttpGet("by-cinema/{cinemaId:guid}")]
    public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetByCinemaId(Guid cinemaId)
    {
        return Ok(await _auditoriumRepository.GetByCinemaIdAsync(cinemaId));
    }

    [HttpPost]
    public async Task<ActionResult<AuditoriumDto>> Create([FromBody] CreateAuditoriumDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _auditoriumRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuditoriumDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _auditoriumRepository.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _auditoriumRepository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}