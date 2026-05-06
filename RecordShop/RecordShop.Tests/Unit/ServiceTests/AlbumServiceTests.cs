using FluentAssertions;
using Moq;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;

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
        public async Task GetAlbumByIdAsync_ShouldNotThrowAnExceptionAndReturnNull_WhenRepoReturnsNull()
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

        [Test]
        public async Task PostAlbumAsync_ShouldCallRepoMethodAndReturnCorrectDTO_WhenDTOIsValid()
        {
            var testDTO = new PostAlbumRequest("Test", "Test", 4M);
            var testAlbum = new Album { Title = "Test", Artist = "Test", Price = 4M };

            _albumRepositoryMock.Setup(a => a.PostAlbumAsync(It.IsAny<Album>())).ReturnsAsync(testAlbum);

            var result = await _albumService.PostAlbumAsync(testDTO);

            _albumRepositoryMock.Verify(a => a.PostAlbumAsync(It.IsAny<Album>()), Times.Once());
            result.Should().BeEquivalentTo(testDTO);
        }

        [Test]
        public async Task PutAlbumAsync_ShouldNotThrowAnExceptionAndReturnNull_WhenGetByIdReturnsNull()
        {
            int id = 99;
            var albumToUpdate = new PutAlbumRequest("Updated Title", "Updated Artist", 15.99M);

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync((Album)null!);

            var result = await _albumService.PutAlbumAsync(albumToUpdate, id);

            result.Should().BeNull();
        }

        [Test]
        public async Task PutAlbumAsync_ShouldCallRepoMethodAndReturnCorrectDTO_WhenDTOIsValid()
        {  
            int id = 1;

            var testDTO = new PutAlbumRequest("New", "New", 4M);
            var existingAlbum = new Album { Id = id, Title = "Old Title", Artist = "Old Artist", Price = 2M };
            var updatedAlbum = new Album { Id = id, Title = "New", Artist = "New", Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(existingAlbum);

            _albumRepositoryMock.Setup(a => a.PutAlbumAsync(It.IsAny<Album>())).ReturnsAsync(updatedAlbum);

            var result = await _albumService.PutAlbumAsync(testDTO, id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(updatedAlbum);

            _albumRepositoryMock.Verify(a => a.PutAlbumAsync(It.IsAny<Album>()), Times.Once());
        }

        [Test]
        public async Task DeleteAlbumByIdAsync_ShouldNotThrowAnExceptionAndReturnFalse_WhenGetByIdReturnsNull()
        {
            int id = 1;

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync((Album)null!);

            var result = await _albumService.DeleteAlbumByIdAync(id);

            result.Should().BeFalse();

            _albumRepositoryMock.Verify(a => a.GetAlbumByIdAsync(id), Times.Once());
        }
    }
}