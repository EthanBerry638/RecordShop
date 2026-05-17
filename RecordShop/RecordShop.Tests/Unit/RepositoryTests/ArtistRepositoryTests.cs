using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Repositories;

namespace RecordShop.Tests.Unit.RepositoryTests
{
    public class ArtistRepositoryTests
    {
        private ArtistRepository _artistRepository;
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

            _context.SeedData();

            _artistRepository = new ArtistRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [Test]
        public async Task GetAllArtistsAsync_ShouldReturnEmptyList_WhenArtistTableIsEmpty()
        {
            var expectedList = new List<Artist>();

            _context.Artists.RemoveRange(_context.Artists);
            await _context.SaveChangesAsync();

            var result = await _artistRepository.GetAllArtistsAsync();

            result.Should().BeEquivalentTo(expectedList);
        }
    }
}
