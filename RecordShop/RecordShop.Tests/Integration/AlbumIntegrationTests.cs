using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using RecordShop.Api.Models.DataModels;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RecordShop.Api.Data;
using Moq;
using RecordShop.Api.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace RecordShop.Tests.Integration
{
    public class AlbumIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllAlbumsAsyncEndpoint_ReturnsOkAndWholeList()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/Album");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var albums = JsonSerializer.Deserialize<List<Album>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            albums.Should().NotBeNull();
            albums.Should().HaveCount(4);
            albums[0].Title.Should().Be("Thriller");
            albums.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAllAlbumsAsyncEndpoint_ReturnsOkAndEmptyList()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<IAlbumRepository>();

                    mockRepo.Setup(r => r.GetAllAlbumsAsync()).ReturnsAsync(new List<Album>());

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("api/Album");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var albums = JsonSerializer.Deserialize<List<Album>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            albums.Should().NotBeNull();
            albums.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAlbumsAsyncEndpoint_ReturnsCorrectMessageFromMiddlewareWhenServerIsDown()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<IAlbumRepository>();

                    mockRepo.Setup(r => r.GetAllAlbumsAsync()).ThrowsAsync(new SqliteException("Connection failed", 10));

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("api/Album");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Server is down :(");
        }

        [Test]
        public async Task GetAlbumByIdAsyncEndpoint_ReturnsOkAndAlbumAtIndex4()
        {
            var client = _factory.CreateClient();
            var testAlbum = new Album { Id = 4, Title = "Nevermind", Artist = "Nirvana", Price = 5.99M };

            var response = await client.GetAsync("api/Album/4");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var album = JsonSerializer.Deserialize<Album>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            album.Should().NotBeNull();
            album.Should().BeEquivalentTo(testAlbum);
        }

        [Test]
        public async Task GetAlbumByIdAsyncEndpoint_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/Album/500");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
