using LeapEventApi.Models;
using NHibernate;

namespace LeapEventApi.Repositories
{
    public interface IEventRepository
    {
        List<Events> GetUpcomingEvents(int days);
        Events GetById(int id);
    }
    public class EventRepository : IEventRepository
    {
        private readonly ISessionFactory _sessionFactory;
        public EventRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<Events> GetUpcomingEvents(int days)
        {
            using var session = _sessionFactory.OpenSession();
            var endDate = DateTime.UtcNow.AddDays(days);
            return session.Query<Events>()
                .Where(e => e.StartsOn >= DateTime.UtcNow && e.StartsOn <= endDate)
                .ToList();
        }

        public Events GetById(int id)
        {
            using var session = _sessionFactory.OpenSession();
            return session.Get<Events>(id);
        }
    }
}
