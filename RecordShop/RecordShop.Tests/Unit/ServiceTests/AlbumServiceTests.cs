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
            var testList = new List<Album>
            {
                new Album
                {
                    Id = 1,
                    Title = "Test Title1",
                    Description = "Description for album 1",
                    ReleaseDate = new DateOnly(2020, 1, 1),
                    Price = 0.00M},
                new Album
                {
                    Id = 2,
                    Title = "Test Title2",
                    Description = "Description for album 2",
                    ReleaseDate = new DateOnly(2021, 5, 20),
                    Price = 0.00M},
                new Album
                {
                     Id = 3,
                     Title = "Test Title3",
                     Description = "Description for album 3",
                     ReleaseDate = new DateOnly(2022, 11, 15),
                     Price = 0.00M},
                new Album
                {
                    Id = 4,
                    Title = "Nevermind",
                    Description = "Description for album 4",
                    ReleaseDate = new DateOnly(1991, 9, 24),
                    Price = 0.00M},
                new Album
                {
                    Id = 5,
                    Title = "Test Title5",
                    Description = "Description for album 5",
                 ReleaseDate = new DateOnly(2023, 3, 10),
                    Price = 0.00M}
            };

            _albumRepositoryMock.Setup(a => a.GetAllAlbumsAsync()).ReturnsAsync(testList);

            var result = await _albumService.GetAllAlbumsAsync();

            result.Should().BeEqualTo(testList);
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
            var testAlbum = new Album { Id = 1, Title = "Test Title1", Description = "Test Desc 1", ReleaseDate = new DateOnly(2002, 2, 2), Price = 0.00M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(testAlbum);

            var result = await _albumService.GetAlbumByIdAsync(id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(testAlbum);
            result.Title.Should().Be("Test Title1");
        }

        [Test]
        public async Task PostAlbumAsync_ShouldCallRepoMethodAndReturnCorrectDTO_WhenDTOIsValid()
        {
            var testDTO = new PostAlbumRequest("Test", "Desc", new DateOnly(2001, 8, 1), 4M);
            var testAlbum = new Album { Title = "Test", Description = "Desc", ReleaseDate =  new DateOnly(2001, 8, 1), Price = 4M };

            _albumRepositoryMock.Setup(a => a.PostAlbumAsync(It.IsAny<Album>())).ReturnsAsync(testAlbum);

            var result = await _albumService.PostAlbumAsync(testDTO);

            _albumRepositoryMock.Verify(a => a.PostAlbumAsync(It.IsAny<Album>()), Times.Once());
            result.Should().BeEquivalentTo(testDTO);
        }

        [Test]
        public async Task PutAlbumAsync_ShouldNotThrowAnExceptionAndReturnNull_WhenGetByIdReturnsNull()
        {
            int id = 99;
            var albumToUpdate = new PutAlbumRequest("Updated Title", "Updated Desc", null, 15.99M);

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync((Album)null!);

            var result = await _albumService.PutAlbumAsync(albumToUpdate, id);

            result.Should().BeNull();
        }

        [Test]
        public async Task PutAlbumAsync_ShouldCallRepoMethodAndReturnCorrectDTO_WhenDTOIsValid()
        {
            int id = 1;

            var testDTO = new PutAlbumRequest("New", "New", null, 4M);
            var existingAlbum = new Album { Id = id, Title = "Old Title", Description = "Old Desc", ReleaseDate = new DateOnly(2001, 1, 1), Price = 2M };
            var updatedAlbum = new Album { Id = id, Title = "New", Description = "New", ReleaseDate = null, Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(existingAlbum);

            _albumRepositoryMock.Setup(a => a.PutAlbumAsync(It.IsAny<Album>())).ReturnsAsync(updatedAlbum);

            var result = await _albumService.PutAlbumAsync(testDTO, id);

            result.Should().NotBeNull();
            result.Title.Should().Be(updatedAlbum.Title);
            result.Description.Should().Be(updatedAlbum.Description);
            result.ReleaseDate.Should().BeNull();
            result.Price.Should().Be(updatedAlbum.Price);

            _albumRepositoryMock.Verify(a => a.PutAlbumAsync(It.IsAny<Album>()), Times.Once());
        }

        [Test]
        public async Task DeleteAlbumByIdAsync_ShouldReturnFalse_WhenGetByIdReturnsNull()
        {
            int id = 1;

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync((Album)null!);

            var result = await _albumService.DeleteAlbumByIdAync(id);

            result.Should().BeFalse();

            _albumRepositoryMock.Verify(a => a.GetAlbumByIdAsync(id), Times.Once());
        }

        [Test]
        public async Task DeleteAlbumByIdAsync_ShouldReturnTrue_WhenGetByIdReturnsAnAlbum()
        {
            int id = 1;
            var deletedAlbum = new Album { Id = id, Title = "Deleted", Description = "Deleted", Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(deletedAlbum);
            _albumRepositoryMock.Setup(a => a.DeleteAlbumByIdAync(id)).ReturnsAsync(true);

            var result = await _albumService.DeleteAlbumByIdAync(id);

            result.Should().BeTrue();

            _albumRepositoryMock.Verify(a => a.GetAlbumByIdAsync(id), Times.Once());
            _albumRepositoryMock.Verify(a => a.DeleteAlbumByIdAync(id), Times.Once());
        }

        [Test]
        public async Task DeleteAlbumByIdAsync_ShouldReturnFalse_WhenGetByIdReturnsAnAlbumButRepositoryReturnsFalse()
        {
            int id = 1;
            var deletedAlbum = new Album { Id = id, Title = "Deleted", Description = "Deleted", Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(deletedAlbum);
            _albumRepositoryMock.Setup(a => a.DeleteAlbumByIdAync(id)).ReturnsAsync(false);

            var result = await _albumService.DeleteAlbumByIdAync(id);

            result.Should().BeFalse();

            _albumRepositoryMock.Verify(a => a.GetAlbumByIdAsync(id), Times.Once());
            _albumRepositoryMock.Verify(a => a.DeleteAlbumByIdAync(id), Times.Once());
        }
    }
}