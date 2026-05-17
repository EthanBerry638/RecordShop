using Moq;
using RecordShop.Api.Repositories;
using RecordShop.Api.Services;

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
    }
}
