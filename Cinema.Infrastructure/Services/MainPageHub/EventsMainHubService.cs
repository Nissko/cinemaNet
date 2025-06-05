using Cinema.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.Infrastructure.Services.MainPageHub;

public class EventsMainHubService : IEventsMainHubService
{
    private readonly IHubContext<EventsMainHub> _hubContext;

    public EventsMainHubService(IHubContext<EventsMainHub> hubContext) => 
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    
    public async Task SendSeatsUpdateAsync(Guid screeningId)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveSeatsUpdate", screeningId);
    }
}