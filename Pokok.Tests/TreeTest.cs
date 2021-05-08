using Moq;
using Xunit;
using Pokok.Models;
using Pokok.Services;

namespace Pokok.Tests
{
    public class TreeTest
    {
        public Mock<TreeService> mock = new Mock<TreeService>();

        [Fact]
        public void GetLocations()
        {

        }

        [Theory]
        [InlineData(1)]
        public void GetLocationById(int id)
        {
            int jk = id + 1;
        }
    }
}
