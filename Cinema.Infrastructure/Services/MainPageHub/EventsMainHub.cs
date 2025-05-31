using Cinema.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.Infrastructure.Services.MainPageHub;

public class EventsMainHub : Hub
{
    private readonly IEventsMainHubService _eventsMainHubService;

    public EventsMainHub(IEventsMainHubService eventsMainHubService) => 
        _eventsMainHubService = eventsMainHubService;

    public async Task NotifySeatsUpdated() =>
        await _eventsMainHubService.SendSeatsUpdateAsync();
}