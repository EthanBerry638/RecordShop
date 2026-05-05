using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Repositories;

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
            var testList = new List<Album> 
            {
                new Album { Id = 1, Title = "Thriller", Artist = "Michael Jackson", Price = 10.99M },
                new Album { Id = 2, Title = "Back In Black", Artist = "AC/DC", Price = 9.49M },
                new Album { Id = 3, Title = "The Dark Side of the Moon", Artist = "Pink Floyd", Price = 15.00M },
                new Album { Id = 4, Title = "Nevermind", Artist = "Nirvana", Price = 5.99M }
            };

            var result = await _albumRepository.GetAllAlbumsAsync();

            result.Should().BeEquivalentTo(testList);
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
    }
}
