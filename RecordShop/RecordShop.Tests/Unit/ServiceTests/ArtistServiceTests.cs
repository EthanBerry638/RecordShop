using FluentAssertions;
using Moq;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;
using System.Threading.Tasks;

namespace RecordShop.Tests.Unit.ServiceTests
{
    public class ArtistServiceTests
    {
        private Mock<IArtistRepository> _artistRepositoryMock;
        private ArtistService _artistService;

        [SetUp]
        public void Setup()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _artistService = new ArtistService(_artistRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllArtistsAsync_ShouldReturnEmptyDTOList_WhenRepoMethodIsCalledAndDBIsNotSeeded()
        {
            var expectedList = new List<Artist>();

            _artistRepositoryMock.Setup(a => a.GetAllArtistsAsync()).ReturnsAsync(expectedList);

            var result = await _artistService.GetAllArtistsAsync();

            result.Should().BeEmpty();
            result.Should().BeEquivalentTo(expectedList);

            _artistRepositoryMock.Verify(a => a.GetAllArtistsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAllArtistsAsync_ShouldReturnSeededDTOList_WhenRepoMethodIsCalledAndDBIsSeeded()
        {
            var expectedList = new List<Artist>
            {
                new() { Id = 1, Name = "Test Artist1", Bio = "Blah blah", Age = 20},
                new() { Id = 2, Name = "Test Artist2", Bio = "Blah blah", Age = 20},
                new() { Id = 3, Name = "Test Artist3", Bio = "Blah blah", Age = 20},
                new() { Id = 4, Name = "Test Artist4", Bio = "Blah blah", Age = 20},
                new() { Id = 5, Name = "Test Artist5", Bio = "Blah blah", Age = 20}
            };

            var expectedDtos = new List<GetArtistResponse>
            {
                new(1,"Test Artist1","Blah blah",20),
                new(2,"Test Artist2","Blah blah",20),
                new(3,"Test Artist3","Blah blah",20),
                new(4,"Test Artist4","Blah blah",20),
                new(5,"Test Artist5","Blah blah",20)
            };

            _artistRepositoryMock.Setup(a => a.GetAllArtistsAsync()).ReturnsAsync(expectedList);

            var result = await _artistService.GetAllArtistsAsync();

            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(expectedDtos);
            result.Count.Should().Be(5);

            _artistRepositoryMock.Verify(a => a.GetAllArtistsAsync(), Times.Once());
        }
    }
}
