using LeapEventApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeapEventApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public IActionResult Get([FromQuery] int days = 30)
        {
            var events = _eventService.GetUpcomingEvents(days);
            return Ok(events);
        }

        [HttpGet("{eventId}/tickets")]
        public IActionResult GetTickets(int eventId)
        {
            var tickets = _ticketService.GetTicketsForEvent(eventId);
            return Ok(tickets);
        }

        [HttpGet("top-selling/count")]
        public IActionResult TopSellingByCount()
        {
            var events = _ticketService.GetTopSellingEventsByCount();
            return Ok(events);
        }

        [HttpGet("top-selling/revenue")]
        public IActionResult TopSellingByRevenue()
        {
            var events = _ticketService.GetTopSellingEventsByRevenue();
            return Ok(events);
        }
    }
}
