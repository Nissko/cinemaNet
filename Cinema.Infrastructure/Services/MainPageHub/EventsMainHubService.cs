using Cinema.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.Infrastructure.Services.MainPageHub;

public class EventsMainHubService : IEventsMainHubService
{
    private readonly IHubContext<EventsMainHub> _hubContext;

    public EventsMainHubService(IHubContext<EventsMainHub> hubContext) => 
        _hubContext = hubContext;
    
    public async Task SendSeatsUpdateAsync()
    {
        await _hubContext.Clients.All.SendAsync("ReceiveSeatsUpdate");
    }
}