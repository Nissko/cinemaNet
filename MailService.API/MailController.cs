using MailService.Application.Common;
using MailService.Domain.Aggregates.Mails;
using Microsoft.AspNetCore.Mvc;

namespace MailService.API
{
    [Microsoft.AspNetCore.Components.Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMailAsync(MailEntity mailData)
        {
            bool result = await _mailService.SendAsync(mailData, CancellationToken.None);

            return result
                ? StatusCode(StatusCodes.Status200OK, "Чек об оплате отправлен.")
                : StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при отправке.");
        }
    }
}