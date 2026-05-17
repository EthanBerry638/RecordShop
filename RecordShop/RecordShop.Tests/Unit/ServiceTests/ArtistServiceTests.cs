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
        public async Task GetAllArtistsAsync_ShouldReturnEmptyDTOList_WhenRepoMethodIsCalled()
        {
            var expectedList = new List<Artist>();

            _artistRepositoryMock.Setup(a => a.GetAllArtistsAsync()).ReturnsAsync(expectedList);

            var result = await _artistService.GetAllArtistsAsync();

            result.Should().BeEmpty();
            result.Should().BeEquivalentTo(expectedList);

            _artistRepositoryMock.Verify(a => a.GetAllArtistsAsync(), Times.Once());
        }
    }
}
