using Microsoft.AspNetCore.Mvc.Testing;

namespace RecordShop.Tests.Integration
{
    public class ArtistIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }


    }
}
