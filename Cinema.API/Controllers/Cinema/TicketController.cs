using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces.Services;
using Cinema.Application.DTO.Ticket;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers.Cinema
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMailServiceClientCore _mailServiceClientCore;

        public TicketController(ITicketRepository ticketRepository, IMailServiceClientCore mailServiceClientCore)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mailServiceClientCore = mailServiceClientCore ?? throw new ArgumentNullException(nameof(mailServiceClientCore));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
        {
            return Ok(await _ticketRepository.GetAllAsync());
        }
    
        [HttpGet("email/{email}/status/{statusText}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetByEmailAndStatus(string email, string statusText)
        {
            if (!Enum.TryParse<TicketStatus>(statusText, true, out var statusEnum))
                return BadRequest(
                    $"Invalid status. Valid values are: {string.Join(", ", Enum.GetNames(typeof(TicketStatus)))}");

            return Ok(await _ticketRepository.GetByEmailAndStatus(email, statusEnum));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TicketDto>> GetById(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return ticket is null ? NotFound() : Ok(ticket);
        }
    
        /// <summary>
        /// Метод для получения занятых мест через билеты
        /// </summary>
        [HttpGet("by-screening/{screeningId:guid}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetScreeningId(Guid screeningId)
        {
            return Ok(await _ticketRepository.GetByScreeningIdAsync(screeningId));
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

        /// <summary>
        /// Покупка
        /// </summary>
        [HttpPost("purchase")]
        public async Task<ActionResult<TicketDto>> PurchaseTicket([FromBody] PurchaseTicketDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var ticket = await _ticketRepository.PurchaseTicketAsync(dto);

            await _mailServiceClientCore.SendEmailAsync(new MailServiceDto()
            {
                To = ["skibko.nik@mail.ru"],
                Bcc = ["nikita.skibko@yandex.ru"],
                Cc = ["nikita.skibko@yandex.ru"],
                From = "nikita.skibko@yandex.ru",
                DisplayName = "AIB-CINEMA",
                Subject = "Чек об оплате AIB-CINEMA ",
                Body = "Вы успешно оплатили",
            });
            
            //return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var deleted = await _ticketRepository.CancelTicketAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}