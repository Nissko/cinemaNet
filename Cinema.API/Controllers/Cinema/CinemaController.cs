using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.DTO.Cinema;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema;

[ApiController]
[Route("api/[controller]")]
public class CinemaController : ControllerBase
{
    private readonly ICinemaRepository _cinemaRepository;

    public CinemaController(ICinemaRepository cinemaRepository)
    {
        _cinemaRepository = cinemaRepository ?? throw new ArgumentNullException(nameof(cinemaRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CinemaDto>>> GetAll()
    {
        return Ok(await _cinemaRepository.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CinemaDto>> GetById(Guid id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        return cinema is null ? NotFound() : Ok(cinema);
    }

    [HttpPost]
    public async Task<ActionResult<CinemaDto>> Create([FromBody] CreateCinemaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _cinemaRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCinemaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _cinemaRepository.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _cinemaRepository.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CinemaDto>>> Search(string name)
    {
        return Ok(await _cinemaRepository.SearchByNameAsync(name));
    }
}