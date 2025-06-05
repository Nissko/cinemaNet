namespace Cinema.Application.Common.Interfaces
{
    public interface IEventsMainHubService
    {
        Task SendSeatsUpdateAsync(Guid screeningId);
    }
}