using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.DTO.Screening;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema;

[ApiController]
[Route("[controller]")]
public class ScreeningController : ControllerBase
{
    private readonly IScreeningRepository _screeningRepository;

    public ScreeningController(IScreeningRepository screeningRepository)
    {
        _screeningRepository = screeningRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetAll()
    {
        return Ok(await _screeningRepository.GetAllAsync());
    }
    
    [HttpGet("by-day/{date:datetime}")]
    public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetByDayAll(DateTime date)
    {
        return Ok(await _screeningRepository.GetByDayAsync(date));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScreeningDto>> GetById(Guid id)
    {
        var screening = await _screeningRepository.GetByIdAsync(id);
        return screening is null ? NotFound() : Ok(screening);
    }

    [HttpGet("by-movie/{movieId:guid}")]
    public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetByMovieId(Guid movieId)
    {
        return Ok(await _screeningRepository.GetByMovieIdAsync(movieId));
    }

    [HttpGet("by-auditorium/{auditoriumId:guid}")]
    public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetByAuditoriumId(Guid auditoriumId)
    {
        return Ok(await _screeningRepository.GetByAuditoriumIdAsync(auditoriumId));
    }

    [HttpPost]
    public async Task<ActionResult<ScreeningDto>> Create([FromBody] CreateScreeningDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var created = await _screeningRepository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScreeningDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updated = await _screeningRepository.UpdateAsync(id, dto);
            
            return updated ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _screeningRepository.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}