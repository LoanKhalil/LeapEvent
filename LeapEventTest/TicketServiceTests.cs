using LeapEventApi.Models;
using LeapEventApi.Repositories;
using LeapEventApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapEventTest
{
    public class TicketServiceTests
    {
        private Mock<ITicketRepository> _ticketRepoMock;
        private TicketService _ticketService;

        [SetUp]
        public void SetUp()
        {
            _ticketRepoMock = new Mock<ITicketRepository>();
            _ticketService = new TicketService(_ticketRepoMock.Object);
        }

        [Test]
        public void GetTicketsForEvent_ShouldReturnTickets()
        {
            // Arrange
            var eventId = "event-123";
            var mockTickets = new List<TicketSales>
            {
                new TicketSales { Id = "123", EventId = eventId, PriceInCents = 5000 },
                new TicketSales { Id = "124", EventId = eventId, PriceInCents = 6000 }
            };

            _ticketRepoMock.Setup(r => r.GetTicketsForEvent(eventId)).Returns(mockTickets);

            // Act
            var result = _ticketService.GetTicketsForEvent(eventId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(5000, result[0].PriceInCents);
        }

        [Test]
        public void GetTopSellingEventsByVolume_ShouldReturnList()
        {
            // Arrange
            var mockEvents = new List<EventsWithVolume>
            {
                new EventsWithVolume { Id = "123", Name = "Event A", TotalVolume = 100 },
                new EventsWithVolume { Id = "124", Name = "Event B", TotalVolume = 90 }
            };

            _ticketRepoMock.Setup(r => r.GetTopSellingEventsByCount()).Returns(mockEvents);

            // Act
            var result = _ticketService.GetTopSellingEventsByVolume();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Event A", result[0].Name);
        }

        [Test]
        public void GetTopSellingEventsByRevenue_ShouldReturnList()
        {
            // Arrange
            var mockEvents = new List<EventsWithRevenue>
            {
                new EventsWithRevenue { Id = "123", Name = "Event A", TotalRevenue = 100000 },
                new EventsWithRevenue { Id = "124", Name = "Event B", TotalRevenue = 80000 }
            };

            _ticketRepoMock.Setup(r => r.GetTopSellingEventsByRevenue()).Returns(mockEvents);

            // Act
            var result = _ticketService.GetTopSellingEventsByRevenue();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100000, result[0].TotalRevenue);
        }
    }
}
