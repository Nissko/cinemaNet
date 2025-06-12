using System.Net.Http.Json;
using Cinema.Application.Common.Interfaces.Services;
using Cinema.Application.DTO.Ticket;

namespace Cinema.Infrastructure.Services.Services
{
    public class MailServiceClientCore : IMailServiceClientCore
    {
        private readonly HttpClient _httpClient;
        //TODO: указание Dev/Host
        private static string _url = "https://aib-cinema.ru/mailservice/send-mail";
        //private static string _url = "http://localhost:5030/send-mail";

        public MailServiceClientCore(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task SendEmailAsync(MailServiceDto mailServiceDto)
        {
            await _httpClient.PostAsJsonAsync(_url, mailServiceDto);
        }
    }
}