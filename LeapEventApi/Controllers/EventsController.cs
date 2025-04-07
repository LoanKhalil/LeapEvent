using LeapEventApi.Models;
using LeapEventApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeapEventApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;

        public EventsController(IEventService eventService, ITicketService ticketService)
        {
            _eventService = eventService;
            _ticketService = ticketService;
        }

        [HttpGet("{days}")]
        public ApiResponse<IEnumerable<Events>> Get(int days = 30)
        {
            try
            { var events = _eventService.GetUpcomingEvents(days);
                return new ApiResponse<IEnumerable<Events>>(events);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<Events>>(new List<Events>(), true, "Exception retrieving events.");
            }

        }

        [HttpGet("{eventId}/tickets")]
        public ApiResponse<IEnumerable<TicketSales>> GetTickets(string eventId)
        {
            try
            {
                var tickets = _ticketService.GetTicketsForEvent(eventId);
                return new ApiResponse<IEnumerable<TicketSales>>(tickets);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<TicketSales>>(new List<TicketSales>(), true,
                    "Exception retrieving ticket sales.");
            }
        }

        [HttpGet("TopSellingEventsByVolume")]
        public ApiResponse<IEnumerable<EventsWithVolume>> TopSellingEventsByVolume()
        {
            try
            {
                var events = _ticketService.GetTopSellingEventsByVolume();
                return new ApiResponse<IEnumerable<EventsWithVolume>>(events);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EventsWithVolume>>(new List<EventsWithVolume>(), true, "Exception retrieving top volume events.");
            }

        }

        [HttpGet("TopSellingEventsByRevenue")]
        public ApiResponse<IEnumerable<EventsWithRevenue>> TopSellingEventsByRevenue()
        {
            try
            {
                var events = _ticketService.GetTopSellingEventsByRevenue();
                return new ApiResponse<IEnumerable<EventsWithRevenue>>(events);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EventsWithRevenue>>(new List<EventsWithRevenue>(), true, "Exception retrieving top revenue events.");
            }

        }
    }
}
