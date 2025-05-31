using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.DTO.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;

    public TicketController(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
    {
        return Ok(await _ticketRepository.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TicketDto>> GetById(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    /// <summary>
    /// Аренда
    /// </summary>
    [HttpPost("book")]
    public async Task<ActionResult<List<Guid>>> BookTicket([FromBody] BookTicketDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ticket = await _ticketRepository.BookTicketAsync(dto);
        return ticket.Count == 0 ? new List<Guid>() : Ok(ticket);
    }

    [HttpPost("purchase")]
    public async Task<ActionResult<TicketDto>> PurchaseTicket([FromBody] PurchaseTicketDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ticket = await _ticketRepository.PurchaseTicketAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var deleted = await _ticketRepository.CancelTicketAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}