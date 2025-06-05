using Cinema.Application.DTO.Ticket;

namespace Cinema.Application.Common.Interfaces.Services;

public interface IMailServiceClientCore
{
    Task SendEmailAsync(MailServiceDto mailServiceDto);
}