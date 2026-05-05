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
        public async Task AlbumController_ShouldReturnOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var testList = new List<Album>();

            _albumServiceMock.Setup(a => a.GetAllAlbums()).ReturnsAsync(testList);

            var result = await _albumController.GetAllAlbums();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<Album>;

            value.Should().NotBeNull();
            value.Should().BeEmpty();
        }
    }
}
