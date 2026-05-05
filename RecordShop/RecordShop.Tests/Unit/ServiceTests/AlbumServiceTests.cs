using Moq;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;
using RecordShop.Api.Models.DataModels;
using FluentAssertions;

namespace RecordShop.Tests.Unit.ServiceTests
{
    public class AlbumServiceTests
    {
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private AlbumService _albumService;

        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllAlbums_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            _albumRepositoryMock.Setup(a => a.GetAllAlbums()).ReturnsAsync((List<Album>?)null);

            var result = await _albumService.GetAllAlbums();

            result.Should().BeEqualTo(null);
        }
    }
}
