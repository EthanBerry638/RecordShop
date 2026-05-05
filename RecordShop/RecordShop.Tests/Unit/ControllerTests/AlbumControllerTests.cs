using Moq;
using RecordShop.Api.Controllers;
using RecordShop.Api.Services;
using RecordShop.Api.Models.DataModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

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

        [Test]
        public async Task GetAllAlbumsAsync_ShouldReturnOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var testList = new List<Album>();

            _albumServiceMock.Setup(a => a.GetAllAlbumsAsync()).ReturnsAsync(testList);

            var result = await _albumController.GetAllAlbumsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<Album>;

            value.Should().NotBeNull();
            value.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldReturnOkWithList_WhenServiceReturnsSeededList()
        {
            var testList = new List<Album>
            {
                new Album {Title = "Test Title1", Artist = "Test Artist1", Price = 0.00M },
                new Album {Title = "Test Title2", Artist = "Test Artist2", Price = 0.00M },
                new Album {Title = "Test Title3", Artist = "Test Artist3", Price = 0.00M },
                new Album {Title = "Test Titl4", Artist = "Test Artist4", Price = 0.00M },
                new Album {Title = "Test Title5", Artist = "Test Artist5", Price = 0.00M },
            };

            _albumServiceMock.Setup(a => a.GetAllAlbumsAsync()).ReturnsAsync(testList);

            var result = await _albumController.GetAllAlbumsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<Album>;

            value.Should().NotBeNull();
            value.Should().BeEquivalentTo(testList);
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldCorrectlyCallServiceOnce_WhenControllerMethodCalled()
        {
            await _albumController.GetAllAlbumsAsync();

            _albumServiceMock.Verify(a => a.GetAllAlbumsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldReturnOkWithAlbum_WhenServiceReturnsAnAlbum()
        {
            var testAlbum = new Album { Id = 2, Title = "TestTitle", Artist = "TestArtist", Price = 12.00M };

            _albumServiceMock.Setup(a => a.GetAlbumByIdAsync(2)).ReturnsAsync(testAlbum);

            var result = await _albumController.GetAlbumByIdAsync(2);

            result.Should().BeOfType <OkObjectResult>();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as Album;

            value.Should().NotBeNull();
            value.Should().BeEquivalentTo(testAlbum);
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldReturnNotFound_WhenServiceReturnsNull()
        {
            _albumServiceMock.Setup(a => a.GetAlbumByIdAsync(100)).ReturnsAsync((Album)null!);

            var result = await _albumController.GetAlbumByIdAsync(100);

            var notFoundResult = result.Should().BeOfType<NotFoundResult>().Subject;
        }

        [Test]
        public async Task GetAllAlbumByIdAsync_ShouldCorrectlyCallServiceOnce_WhenControllerMethodCalled()
        {
            await _albumController.GetAlbumByIdAsync(1);

            _albumServiceMock.Verify(a => a.GetAlbumByIdAsync(1), Times.Once());
        }
    }
}