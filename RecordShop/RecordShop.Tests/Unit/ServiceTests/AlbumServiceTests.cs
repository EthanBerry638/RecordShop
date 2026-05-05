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

        [Test]
        public async Task GetAllAlbums_ShouldReturnList_WhenRepositoryReturnsList()
        {
            var testAlbumList = new List<Album>
            {
                new Album {Title = "Test Title1", Artist = "Test Artist1", Price = 0.00M },
                new Album {Title = "Test Title2", Artist = "Test Artist2", Price = 0.00M },
                new Album {Title = "Test Title3", Artist = "Test Artist3", Price = 0.00M },
                new Album {Title = "Test Titl4", Artist = "Test Artist4", Price = 0.00M },
                new Album {Title = "Test Title5", Artist = "Test Artist5", Price = 0.00M },
            };

            _albumRepositoryMock.Setup(a => a.GetAllAlbums()).ReturnsAsync(testAlbumList);

            var result = await _albumService.GetAllAlbums();

            result.Should().BeEqualTo(testAlbumList);
            result.Should().HaveCount(5);
        }
    }
}
