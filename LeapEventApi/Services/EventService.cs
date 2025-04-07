using LeapEventApi.Models;
using LeapEventApi.Repositories;

namespace LeapEventApi.Services
{
    public interface IEventService
    {
        List<Events> GetUpcomingEvents(int days);
    }

    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public List<Events> GetUpcomingEvents(int days) => _eventRepository.GetUpcomingEvents(days);
    }
}
