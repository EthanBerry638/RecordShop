using Moq;
using RecordShop.Api.Controllers;
using RecordShop.Api.Services;

namespace RecordShop.Tests.Unit.ControllerTests
{
    public class AlbumControllerTests
    {
        private Mock<IAlbumService> _albumServiceMock;
        private AlbumController _albumController;

        [SetUp]
        public void Setup()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _albumController = new AlbumController(_albumServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _albumController.Dispose();
        }
    }
}
