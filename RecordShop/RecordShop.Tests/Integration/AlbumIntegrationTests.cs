using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using RecordShop.Api.Models.DataModels;
using FluentAssertions;

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
        public async Task GetAllBooksAsyncEndpoint_ReturnsOkAndWholeList()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("https://localhost:7091/api/Album");

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
    }
}
