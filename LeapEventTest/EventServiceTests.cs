using LeapEventApi.Repositories;
using LeapEventApi.Services;
using Moq;


namespace LeapEventTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetUpcomingEvents_Returns_CorrectData()
        {
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetUpcomingEvents(It.IsAny<int>()))
                .Returns(new List<LeapEventApi.Models.Events> { new LeapEventApi.Models.Events() { Id = "TicketNum1", Name = "Test" } });

            var service = new EventService(mockRepo.Object);
            var result = service.GetUpcomingEvents(30);

            Assert.IsNotEmpty(result);
            Assert.AreEqual("Test", result[0].Name);
        }
    }
}