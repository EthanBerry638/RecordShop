using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Repositories;
using System.Text.Json;
using RecordShop.Api.Models.DTOs;

namespace RecordShop.Tests.Unit.RepositoryTests
{
    public class AlbumRepositoryTests
    {
        private AlbumRepository _albumRepository;
        private RecordShopContext _context;
        private SqliteConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<RecordShopContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new RecordShopContext(options);

            _context.Database.EnsureCreated();

            _albumRepository = new AlbumRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldReturnListOfAlbums_WhenDatabaseIsSeeded()
        {
            var jsonString = File.ReadAllText("Resources\\albums.json");
            var expectedAlbums = JsonSerializer.Deserialize<List<Album>>(jsonString);

            var result = await _albumRepository.GetAllAlbumsAsync();

            result.Should().BeEquivalentTo(expectedAlbums);
        }

        [Test]
        public async Task GetAllAlbumsAsync_ShouldReturnEmptyList_WhenDatabaseIsEmpty()
        {
            var testList = new List<Album>();

            _context.Albums.RemoveRange(_context.Albums);
            await _context.SaveChangesAsync();

            var result = await _albumRepository.GetAllAlbumsAsync();

            result.Should().BeEquivalentTo(testList);
        }

        [Test]
        public void GetAllAlbumsAsync_ShouldThrowException_WhenDatabaseIsDown()
        {
            _connection.Close();

            Func<Task> act = async () => await _albumRepository.GetAllAlbumsAsync();

            act.Should().ThrowAsync<SqliteException>();
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldReturnNull_WhenDatabaseDoesNotContainAlbumWithSpecifiedId()
        {
            int testId = 1000000;

            var result = await _albumRepository.GetAlbumByIdAsync(testId);

            result.Should().BeNull();
        }

        [Test]
        public async Task GetAlbumByIdAsync_ShouldReturnAlbum_WhenDatabaseDoesAlbumWithSpecifiedId()
        {
            var testAlbum = new Album { Id = 3, Title = "The Dark Side of the Moon", Artist = "Pink Floyd", Price = 15.00M };
            int testId = 3;

            var result = await _albumRepository.GetAlbumByIdAsync(testId);

            result.Should().BeOfType<Album>();
            result.Should().BeEquivalentTo(testAlbum);
        }

        [Test]
        public async Task PostAlbumAsync_ShouldReturnDTO_WhenDTOIsValid()
        {
            var testAlbum = new PostAlbumRequestResponse("test", "test", 40);

            var result = await _albumRepository.PostAlbumAsync(testAlbum);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(testAlbum);
        }
    }
}
