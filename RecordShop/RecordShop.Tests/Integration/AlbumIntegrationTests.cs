using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;
using System.Net.Http.Json;
using System.Text.Json;

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

        [Test]
        public async Task GetAlbumByIdAsyncEndpoint_ReturnsCorrectMessageFromMiddlewareWhenUserMakesBadRequest()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockService = new Mock<IAlbumService>();

                    mockService.Setup(s => s.GetAlbumByIdAsync(0)).ThrowsAsync(new ArgumentException());

                    services.AddScoped(_ => mockService.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("api/Album/0");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task PostAlbumAsyncEndpoint_ReturnsCreatedAtAction()
        {
            var client = _factory.CreateClient();
            var requestDTO = new PostAlbumRequest("Test", "Test", 6);
            var expectedResponseDTO = new PostAlbumResponse(7, "Test", "Test", 6);

            var response = await client.PostAsJsonAsync("api/Album/add", requestDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.PathAndQuery.Should().Contain($"api/Album/{expectedResponseDTO.Id}");

            var content = await response.Content.ReadAsStringAsync();

            var album = JsonSerializer.Deserialize<Album>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            album.Should().NotBeNull();
            album.Should().BeEquivalentTo(expectedResponseDTO);
        }

        [Test]
        public async Task PostAlbumAsyncEndpoint_ReturnsBadRequestWithNegativePrice()
        {
            var client = _factory.CreateClient();
            var requestDTO = new PostAlbumRequest("Test", "Test", -1);

            var response = await client.PostAsJsonAsync("api/Album/add", requestDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task PutAlbumAsyncEndpoint_ReturnsCreatedAtAction()
        {
            var client = _factory.CreateClient();
            int id = 3;
            var requestDTO = new PutAlbumRequest("Test", "Test", 6);
            var expectedResponseDTO = new PutAlbumResponse(id, "Test", "Test", 6);

            var response = await client.PutAsJsonAsync($"api/Album/replace/{id}", requestDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location.PathAndQuery.Should().Contain($"api/Album/{expectedResponseDTO.Id}");

            var content = await response.Content.ReadAsStringAsync();

            var album = JsonSerializer.Deserialize<Album>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            album.Should().NotBeNull();
            album.Should().BeEquivalentTo(expectedResponseDTO);
        }

        [Test]
        public async Task PutAlbumAsyncEndpoint_ReturnsBadRequestWithNegativePrice()
        {
            var client = _factory.CreateClient();
            int id = 3;
            var requestDTO = new PutAlbumRequest("Test", "Test", -1);

            var response = await client.PutAsJsonAsync($"api/Album/replace/{id}", requestDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task PutAlbumAsyncEndpoint_ReturnsNotFoundWithIdThatDoesNotExist()
        {
            var client = _factory.CreateClient();
            int id = 10000000;
            var requestDTO = new PutAlbumRequest("Test", "Test", 3);

            var response = await client.PutAsJsonAsync($"api/Album/replace/{id}", requestDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
