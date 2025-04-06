using LeapEventApi.Models;
using LeapEventApi.Repositories;
using NHibernate;

namespace LeapEventApi.Services
{
    public interface ITicketService
    {
        IList<Ticket> GetTicketsForEvent(int eventId);
        IList<Event> GetTopSellingEventsByCount();
        IList<Event> GetTopSellingEventsByRevenue();
    }

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IList<Ticket> GetTicketsForEvent(int eventId)
        {
            return _ticketRepository.GetTicketsForEvent(eventId);
        }

        public IList<Event> GetTopSellingEventsByCount()
        {
            return _ticketRepository.GetTopSellingEventsByCount();
        }

        public IList<Event> GetTopSellingEventsByRevenue()
        {
            return _ticketRepository.GetTopSellingEventsByRevenue();
        }
    }
}
