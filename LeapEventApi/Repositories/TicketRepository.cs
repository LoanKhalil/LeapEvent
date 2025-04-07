using LeapEventApi.Models;
using NHibernate;

namespace LeapEventApi.Repositories
{
    public interface ITicketRepository
    {
        IList<TicketSales> GetTicketsForEvent(string eventId);

        IList<EventsWithVolume> GetTopSellingEventsByCount();

        IList<EventsWithRevenue> GetTopSellingEventsByRevenue();
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public TicketRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IList<TicketSales> GetTicketsForEvent(string eventId)
        {
            using var session = _sessionFactory.OpenSession();
            return session.Query<TicketSales>().Where(t => t.EventId == eventId).ToList();
        }

        public IList<EventsWithRevenue> GetTopSellingEventsByRevenue()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                // Step 1: Get total revenue per event
                var totalRevenues = session.Query<TicketSales>()
                    .GroupBy(t => t.EventId)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        TotalRevenue = g.Sum(t => t.PriceInCents)
                    })
                    .ToList();

                // Step 2: Get top 5 event IDs by revenue
                var topRevenues = totalRevenues
                    .OrderByDescending(x => x.TotalRevenue)
                    .Take(5)
                    .ToList();

                var topEventIds = topRevenues.Select(x => x.EventId).ToList();

                // Step 3: Fetch corresponding events
                var events = session.Query<Events>()
                    .Where(e => topEventIds.Contains(e.Id))
                    .ToList();

                // Step 4: Merge events with revenue
                var result = events
                    .Join(topRevenues,
                        e => e.Id,
                        r => r.EventId,
                        (e, r) => new EventsWithRevenue
                        {
                            Id = e.Id,
                            Name = e.Name,
                            StartsOn = e.StartsOn,
                            EndsOn = e.EndsOn,
                            Location = e.Location,
                            TotalRevenue = r.TotalRevenue
                        }).OrderByDescending(x => x.TotalRevenue)
                    .ToList();

                return result;
            }
        
        }

        public IList<EventsWithVolume> GetTopSellingEventsByCount()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                
                var totalVolume = session.Query<TicketSales>()
                    .GroupBy(t => t.EventId).Select(g =>
                    new {
                        EventId = g.Key,
                        Volume = g.Count()
                    }).ToList();


                var topVolumes = totalVolume
                    .OrderByDescending(x => x.Volume)
                    .Take(5)
                    .ToList();

                var topEventIds = topVolumes.Select(x => x.EventId).ToList();

                var events = session.Query<Events>()
                    .Where(e => topEventIds.Contains(e.Id))
                    .ToList();


                var result = events
                    .Join(topVolumes,
                        e => e.Id,
                        r => r.EventId,
                        (e, r) => new EventsWithVolume()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            StartsOn = e.StartsOn,
                            EndsOn = e.EndsOn,
                            Location = e.Location,
                            TotalVolume = r.Volume
                        }).OrderByDescending(x => x.TotalVolume)
                    .ToList();

                return result;
            }
        }

    }
}
