using Moq;
using RecordShop.Api.Controllers;
using RecordShop.Api.Services;

namespace RecordShop.Tests.Unit.ControllerTests
{
    public class ArtistControllerTests
    {
        private Mock<IArtistService> _mockService;
        private ArtistController _artistController;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IArtistService>();
            _artistController = new ArtistController(_mockService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _artistController.Dispose();
        }
    }
}
