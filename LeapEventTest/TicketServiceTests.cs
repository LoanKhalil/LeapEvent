using LeapEventApi.Models;
using LeapEventApi.Repositories;
using LeapEventApi.Services;
using Moq;
using NHibernate;

namespace LeapEventTest
{
    public class TicketServiceTests
    {

        private TicketService _ticketService;
        

        private Mock<ISessionFactory> _mockSessionFactory;
        private Mock<ISession> _mockSession;
        private Mock<IQueryable<TicketSales>> _mockTicketSalesQuery;
        private Mock<IQueryable<Events>> _mockEventsQuery;
        private Mock<ITicketRepository> _mockTicketRepository;

        [SetUp]
        public void SetUp()
        {
            
            
            // Mock ISessionFactory and ISession
            _mockSessionFactory = new Mock<ISessionFactory>();
            _mockSession = new Mock<ISession>();

            // Mock the TicketSales query
            _mockTicketSalesQuery = new Mock<IQueryable<TicketSales>>();
            _mockSession.Setup(s => s.Query<TicketSales>()).Returns(_mockTicketSalesQuery.Object);

            // Mock the Events query
            _mockEventsQuery = new Mock<IQueryable<Events>>();
            _mockSession.Setup(s => s.Query<Events>()).Returns(_mockEventsQuery.Object);

            // Mock session factory to return our mocked session
            _mockSessionFactory.Setup(f => f.OpenSession()).Returns(_mockSession.Object);

            
            _mockTicketRepository = new Mock<ITicketRepository>();

            // Initialize the TicketService with the mocked session factory
            _ticketService = new TicketService(_mockTicketRepository.Object);

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

            _mockTicketRepository.Setup(r => r.GetTicketsForEvent(eventId)).Returns(mockTickets);

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

            _mockTicketRepository.Setup(r => r.GetTopSellingEventsByCount()).Returns(mockEvents);

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

            _mockTicketRepository.Setup(r => r.GetTopSellingEventsByRevenue()).Returns(mockEvents);

            // Act
            var result = _ticketService.GetTopSellingEventsByRevenue();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100000, result[0].TotalRevenue);
        }

        [Test]
        public void GetTopSellingEventsByRevenue_ReturnsTop5EventsByRevenue()
        {
            // Arrange: Setup mock data
            var ticketSales = new List<TicketSales>
            {
                new TicketSales { EventId = "1", PriceInCents = 100 },
                new TicketSales { EventId = "1", PriceInCents = 200 },
                new TicketSales { EventId = "2", PriceInCents = 50 },
                new TicketSales { EventId = "3", PriceInCents = 300 },
                new TicketSales { EventId = "4", PriceInCents = 150 },
                new TicketSales { EventId = "5", PriceInCents = 400 },
            };

            var events = new List<Events>
            {
                new Events { Id = "1", Name = "Event 1", StartsOn = DateTime.Now, EndsOn = DateTime.Now.AddHours(2), Location = "Location 1" },
                new Events { Id = "2", Name = "Event 2", StartsOn = DateTime.Now, EndsOn = DateTime.Now.AddHours(2), Location = "Location 2" },
                new Events { Id = "3", Name = "Event 3", StartsOn = DateTime.Now, EndsOn = DateTime.Now.AddHours(2), Location = "Location 3" },
                new Events { Id = "4", Name = "Event 4", StartsOn = DateTime.Now, EndsOn = DateTime.Now.AddHours(2), Location = "Location 4" },
                new Events { Id = "5", Name = "Event 5", StartsOn = DateTime.Now, EndsOn = DateTime.Now.AddHours(2), Location = "Location 5" }
            };

            // Setup the mock queries to return the data
            _mockTicketSalesQuery.Setup(ts => ts.Provider).Returns(ticketSales.AsQueryable().Provider);
            _mockTicketSalesQuery.Setup(ts => ts.Expression).Returns(ticketSales.AsQueryable().Expression);
            _mockTicketSalesQuery.Setup(ts => ts.ElementType).Returns(ticketSales.AsQueryable().ElementType);
            _mockTicketSalesQuery.Setup(ts => ts.GetEnumerator()).Returns(ticketSales.GetEnumerator());

            _mockEventsQuery.Setup(e => e.Provider).Returns(events.AsQueryable().Provider);
            _mockEventsQuery.Setup(e => e.Expression).Returns(events.AsQueryable().Expression);
            _mockEventsQuery.Setup(e => e.ElementType).Returns(events.AsQueryable().ElementType);
            _mockEventsQuery.Setup(e => e.GetEnumerator()).Returns(events.GetEnumerator());


            // Act: Call the method to test
            var ticketService = new TicketService(new TicketRepository(_mockSessionFactory.Object));
            var result = ticketService.GetTopSellingEventsByRevenue();

            // Assert: Check that the results are correct
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("5", result[0].Id); // Event with the highest revenue
            Assert.AreEqual("1", result[1].Id); // Next highest revenue
            Assert.AreEqual("3", result[2].Id); // Next
            Assert.AreEqual("4", result[3].Id);
            Assert.AreEqual("2", result[4].Id);
        }
    }

}

