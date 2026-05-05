using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Repositories;

namespace RecordShop.Tests.Unit.RepositoryTests
{
    public class RepositoryTests
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
    }
}
