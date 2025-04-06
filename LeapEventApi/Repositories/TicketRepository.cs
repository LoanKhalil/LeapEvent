using LeapEventApi.Models;
using NHibernate;

namespace LeapEventApi.Repositories
{
    public interface ITicketRepository
    {
        IList<Ticket> GetTicketsForEvent(int eventId);

        IList<Event> GetTopSellingEventsByCount();
        IList<Event> GetTopSellingEventsByRevenue();

    }

    public class TicketRepository : ITicketRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public TicketRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IList<Ticket> GetTicketsForEvent(int eventId)
        {
            using var session = _sessionFactory.OpenSession();
            return session.Query<Ticket>().Where(t => t.EventId == eventId).ToList();
        }

        public IList<Event> GetTopSellingEventsByCount()
        {
            using var session = _sessionFactory.OpenSession();
            return session.Query<Ticket>()
                .GroupBy(t => t.EventId)
                .OrderByDescending(g => g.Sum(t => t.QuantitySold))
                .Take(5)
                .Join(session.Query<Event>(),
                    t => t.Key,
                    e => e.Id,
                    (g, e) => e)
                .ToList();
        }

        public IList<Event> GetTopSellingEventsByRevenue()
        {
            using var session = _sessionFactory.OpenSession();
            return session.Query<Ticket>()
                .GroupBy(t => t.EventId)
                .OrderByDescending(g => g.Sum(t => t.Price * t.QuantitySold))
                .Take(5)
                .Join(session.Query<Event>(),
                    t => t.Key,
                    e => e.Id,
                    (g, e) => e)
                .ToList();
        }
    }
}
