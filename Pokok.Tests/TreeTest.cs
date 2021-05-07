using Moq;
using Xunit;
using Pokok.Models;
using Pokok.Services;
using Pokok.Interfaces;

namespace Pokok.Tests
{
    public class TreeTest
    {
        public Mock<ITreeService> mock = new Mock<ITreeService>();

        [Fact]
        public void GetLocations()
        {

        }

        [Theory]
        [InlineData(1)]
        public void GetLocationById(int id)
        {

        }
    }
}
