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
        public async Task GetAllAlbumsAsync_ShouldReturnList_WhenRepositoryReturnsList()
        {
            var testAlbumList = new List<Album>
            {
                new Album {Title = "Test Title1", Artist = "Test Artist1", Price = 0.00M },
                new Album {Title = "Test Title2", Artist = "Test Artist2", Price = 0.00M },
                new Album {Title = "Test Title3", Artist = "Test Artist3", Price = 0.00M },
                new Album {Title = "Test Titl4", Artist = "Test Artist4", Price = 0.00M },
                new Album {Title = "Test Title5", Artist = "Test Artist5", Price = 0.00M },
            };

            _albumRepositoryMock.Setup(a => a.GetAllAlbumsAsync()).ReturnsAsync(testAlbumList);

            var result = await _albumService.GetAllAlbumsAsync();

            result.Should().BeEqualTo(testAlbumList);
            result.Should().HaveCount(5);
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldReturnEmptyList_WhenRepositoryReturnsEmptyList()
        {
            var testAlbumList = new List<Album>();
            _albumRepositoryMock.Setup(a => a.GetAllAlbumsAsync()).ReturnsAsync(testAlbumList);

            var result = await _albumService.GetAllAlbumsAsync();

            result.Should().BeEqualTo(testAlbumList);
            result.Should().HaveCount(0);
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldOnlyCallRepoOnce_WhenCalled()
        {
            var result = await _albumService.GetAllAlbumsAsync();

            _albumRepositoryMock.Verify(a => a.GetAllAlbumsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldOnlyCallRepoOnce_WhenCalled()
        {
            var result = await _albumService.GetAlbumByIdAsync(3);

            _albumRepositoryMock.Verify(a => a.GetAlbumByIdAsync(3), Times.Once());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public async Task GetAlbumByIdAsync_ShouldThrowException_WhenInvalidIdPassedIn(int id)
        {
            var act = () => _albumService.GetAlbumByIdAsync(id);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldNotThrowAnExceptionWhenRepoReturnsNull()
        {
            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(10)).ReturnsAsync((Album)null!);

            var result = await _albumService.GetAlbumByIdAsync(10);

            result.Should().BeNull();
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldReturnAlbum_WhenAlbumFoundByRepo()
        {
            int id = 1;
            var testAlbum = new Album { Id = 1, Title = "Test Title1", Artist = "Test Artist1", Price = 0.00M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(testAlbum);

            var result = await _albumService.GetAlbumByIdAsync(id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(testAlbum);
            result.Title.Should().Be("Test Title1");
        }
    }
}
