using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Api.Controllers;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Services;

namespace RecordShop.Tests.Unit.ControllerTests
{
    public class ArtistControllerTests
    {
        private Mock<IArtistService> _artistServiceMock;
        private ArtistController _artistController;

        [SetUp]
        public void Setup()
        {
            _artistServiceMock = new Mock<IArtistService>();
            _artistController = new ArtistController(_artistServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _artistController.Dispose();
        }

        [Test]
        public async Task GetAllArtistsAsync_ShouldReturnOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var testList = new List<GetArtistResponse>();

            _artistServiceMock.Setup(a => a.GetAllArtistsAsync()).ReturnsAsync(testList);

            var result = await _artistController.GetAllArtistsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<GetAlbumResponse>;

            value.Should().NotBeNull();
            value.Should().BeEmpty();
        }
    }
}
