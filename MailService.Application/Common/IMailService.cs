
using MailService.Domain.Aggregates.Mails;

namespace MailService.Application.Common
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailEntity mailData, CancellationToken cancellationToken);
    }
}