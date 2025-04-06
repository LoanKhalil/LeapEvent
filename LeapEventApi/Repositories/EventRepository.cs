using LeapEventApi.Models;
using NHibernate;

namespace LeapEventApi.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetUpcomingEvents(int days);
        Event GetById(int id);
    }
    public class EventRepository : IEventRepository
    {
        private readonly ISessionFactory _sessionFactory;
        public EventRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<Event> GetUpcomingEvents(int days)
        {
            using var session = _sessionFactory.OpenSession();
            var endDate = DateTime.UtcNow.AddDays(days);
            return session.Query<Event>()
                .Where(e => e.StartDateTime >= DateTime.UtcNow && e.StartDateTime <= endDate)
                .ToList();
        }

        public Event GetById(int id)
        {
            using var session = _sessionFactory.OpenSession();
            return session.Get<Event>(id);
        }
    }
}
