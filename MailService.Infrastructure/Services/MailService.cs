using MailKit.Net.Smtp;
using MailKit.Security;
using MailService.Application.Common;
using MailService.Domain.Aggregates.Mails;
using MailService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailService.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettins;

        public MailService(IOptions<MailSettings> mailSettins)
        {
            _mailSettins = mailSettins.Value;
        }

        public async Task<bool> SendAsync(MailEntity mailData, CancellationToken cancellationToken)
        {
            try
            {
                var mail = new MimeMessage();
            
                #region Отправитель / получатель
                // Sender
                mail.From.Add(new MailboxAddress(_mailSettins.DisplayName, mailData.From ?? _mailSettins.From));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _mailSettins.DisplayName, mailData.From ?? _mailSettins.From);

                // Receiver
                foreach (string mailAddress in mailData.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if(!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                if (mailData.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                // CC
                // Check if a CC address was supplied in the request
                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion
            
                #region Содержимое

                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Отправка сообщения
            
                using (var client = new SmtpClient()) {
                    try {
                        await client.ConnectAsync(_mailSettins.Host, _mailSettins.Port, SecureSocketOptions.SslOnConnect, cancellationToken);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(_mailSettins.UserName, _mailSettins.Password, cancellationToken);
                        await client.SendAsync(mail);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally {
                        await client.DisconnectAsync(true);
                        client.Dispose();
                    }
                }

                #endregion

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}