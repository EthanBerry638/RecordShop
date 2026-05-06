using FluentAssertions;
using Moq;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;
using RecordShop.Api.CustomExceptions;

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
        public async Task PostAlbumAsync_ShouldThrowException_WhenPriceIsNegative()
        {
            var testDTO = new PostAlbumRequest("Test", "Test", -1);

            var act = () => _albumService.PostAlbumAsync(testDTO);

            await act.Should().ThrowAsync<InvalidPriceException>();
        }

        [Test]
        [TestCase(2000000)]
        [TestCase(2000000000)]
        [TestCase(2000001)]
        public async Task PostAlbumAsync_ShouldThrowException_WhenPriceIsTooHigh(decimal price)
        {
            var testDTO = new PostAlbumRequest("Test", "Test", price);

            var act = () => _albumService.PostAlbumAsync(testDTO);

            await act.Should().ThrowAsync<InvalidPriceException>();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("              ")]
        public async Task PostAlbumAsync_ShouldThrowException_WhenTitleIsNullOrEmpty(string? title)
        {
            var testDTO = new PostAlbumRequest(title!, "Test", 2);

            var act = () => _albumService.PostAlbumAsync(testDTO);

            await act.Should().ThrowAsync<EmptyStringException>();
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
        [TestCase("             TestTitle", " TestArtist")]
        [TestCase(" TestTitle", " TestArtist          ")]
        public async Task PostAlbumAsync_ShouldTrimTrailingAndFollowingWhitespace_WhenGivenValidDTOWithExtraWhitespace(string title, string artist)
        {
            var testDTO = new PostAlbumRequest(title, artist, 4M);
            var testAlbum = new Album { Title = "TestTitle", Artist = "TestArtist", Price = 4M };

            _albumRepositoryMock.Setup(a => a.PostAlbumAsync(It.IsAny<Album>())).ReturnsAsync(testAlbum);

            var result = await _albumService.PostAlbumAsync(testDTO);

            _albumRepositoryMock.Verify(a => a.PostAlbumAsync(It.IsAny<Album>()), Times.Once());
            result.Title.Should().Be("TestTitle");
            result.Artist.Should().Be("TestArtist");
            result.Should().NotBeNull();
        }

        [Test]
        public async Task PostAlbumAsync_ShouldThrowException_WhenStringsLongerThan255Chars()
        {
            string longTitle = new string('a', 256);
            string longArtist = new string('a', 256);

            var testDTO = new PostAlbumRequest(longTitle, longArtist, 2);

            var act = () => _albumService.PostAlbumAsync(testDTO);

            await act.Should().ThrowAsync<LongStringException>();
        }

        [Test]
        public async Task PostAlbumAsync_ShouldNotThrowException_WhenStringsLessThanOrEqual255Chars()
        {
            string longTitle = new string('a', 255);
            string longArtist = new string('a', 255);

            var testDTO = new PostAlbumRequest(longTitle, longArtist, 4M);
            var testAlbum = new Album { Title = longTitle, Artist = longArtist, Price = 4M };

            _albumRepositoryMock.Setup(a => a.PostAlbumAsync(It.IsAny<Album>())).ReturnsAsync(testAlbum);

            var result = await _albumService.PostAlbumAsync(testDTO);

            _albumRepositoryMock.Verify(a => a.PostAlbumAsync(It.IsAny<Album>()), Times.Once());
            result.Should().BeEquivalentTo(testDTO);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public async Task PutAlbumAsync_ShouldThrowException_WhenInvalidIdPassedIn(int id)
        {
            var albumToUpdate = new PutAlbumRequest ("Updated Title", "Updated Artist", 20);

            Func<Task> act = async () => await _albumService.PutAlbumAsync(albumToUpdate, id);

            await act.Should().ThrowAsync<ArgumentException>();
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
        [TestCase(2000000)]
        [TestCase(2000000000)]
        [TestCase(2000001)]
        public async Task PutAlbumAsync_ShouldThrowException_WhenPriceIsTooHigh(decimal price)
        {
            int id = 1;
            var testDTO = new PutAlbumRequest("Test", "Test", price);

            var act = async () => await _albumService.PutAlbumAsync(testDTO, id);

            await act.Should().ThrowAsync<InvalidPriceException>();
        }

        [Test]
        public async Task PutAlbumAsync_ShouldThrowException_WhenPriceIsNegative()
        {
            int id = 1;
            var testDTO = new PutAlbumRequest("Test", "Test", -1);

            var act = () => _albumService.PutAlbumAsync(testDTO, id);

            await act.Should().ThrowAsync<InvalidPriceException>();
        }

        [Test]
        public async Task PutAlbumAsync_ShouldThrowException_WhenStringsLongerThan255Chars()
        {
            string longTitle = new string('a', 256);
            string longArtist = new string('a', 256);
            int id = 1;

            var testDTO = new PutAlbumRequest(longTitle, longArtist, 2);

            var act = () => _albumService.PutAlbumAsync(testDTO, id);

            await act.Should().ThrowAsync<LongStringException>();
        }

        [Test]
        public async Task PutAlbumAsync_ShouldNotThrowException_WhenStringsLessThanOrEqual255Chars()
        {
            string longTitle = new string('a', 255);
            string longArtist = new string('a', 255);
            int id = 1;

            var testDTO = new PutAlbumRequest(longTitle, longArtist, 4M);
            var existingAlbum = new Album { Id = id, Title = "Old Title", Artist = "Old Artist", Price = 2M };
            var updatedAlbum = new Album { Id = id, Title = longTitle, Artist = longArtist, Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(existingAlbum);

            _albumRepositoryMock.Setup(a => a.PutAlbumAsync(It.IsAny<Album>(), id)).ReturnsAsync(updatedAlbum);

            var result = await _albumService.PutAlbumAsync(testDTO, id);

            result.Should().NotBeNull();
            result.Title.Length.Should().Be(255);

            _albumRepositoryMock.Verify(a => a.PutAlbumAsync(It.IsAny<Album>(), id), Times.Once());
        }

        [Test]
        public async Task PutAlbumAsync_ShouldCallRepoMethodAndReturnCorrectDTO_WhenDTOIsValid()
        {  
            int id = 1;

            var testDTO = new PutAlbumRequest("New", "New", 4M);
            var existingAlbum = new Album { Id = id, Title = "Old Title", Artist = "Old Artist", Price = 2M };
            var updatedAlbum = new Album { Id = id, Title = "New", Artist = "New", Price = 4M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(existingAlbum);

            _albumRepositoryMock.Setup(a => a.PutAlbumAsync(It.IsAny<Album>(), id)).ReturnsAsync(updatedAlbum);

            var result = await _albumService.PutAlbumAsync(testDTO, id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(updatedAlbum);

            _albumRepositoryMock.Verify(a => a.PutAlbumAsync(It.IsAny<Album>(), id), Times.Once());
        }

        [Test]
        public async Task PutAlbumAsync_ShouldTrimWhiteSpace_WhenValidRequestHasSpaces()
        {
            int id = 1;
            var testDTO = new PutAlbumRequest("  Trimmed Title  ", "  Artist  ", 10.99M);
            var existingAlbum = new Album { Id = id, Title = "Old", Artist = "Old", Price = 5M };

            _albumRepositoryMock.Setup(a => a.GetAlbumByIdAsync(id)).ReturnsAsync(existingAlbum);

            _albumRepositoryMock.Setup(a => a.PutAlbumAsync(It.IsAny<Album>(), id)).ReturnsAsync(existingAlbum);

            await _albumService.PutAlbumAsync(testDTO, id);

            _albumRepositoryMock.Verify(a => a.PutAlbumAsync(It.Is<Album>(album =>
                album.Title == "Trimmed Title" &&
                album.Artist == "Artist"), id), Times.Once);
        }
    }
}