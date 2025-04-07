using LeapEventApi.Models;
using LeapEventApi.Repositories;

namespace LeapEventApi.Services
{
    public interface ITicketService
    {
        IList<TicketSales> GetTicketsForEvent(string eventId);
        IList<EventsWithVolume> GetTopSellingEventsByVolume();
        IList<EventsWithRevenue> GetTopSellingEventsByRevenue();

    }

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IList<TicketSales> GetTicketsForEvent(string eventId)
        {
            return _ticketRepository.GetTicketsForEvent(eventId);
        }

        public IList<EventsWithVolume> GetTopSellingEventsByVolume()
        {
            return _ticketRepository.GetTopSellingEventsByCount();
        }

        public IList<EventsWithRevenue> GetTopSellingEventsByRevenue()
        {
            return _ticketRepository.GetTopSellingEventsByRevenue();
        }
    }
}
