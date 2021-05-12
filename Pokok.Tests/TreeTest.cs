using Moq;
using Xunit;
using Pokok.Models;
using Pokok.Interfaces;
using Pokok.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;

namespace Pokok.Tests
{
    public class TreeTest
    {
        public Mock<ITreeService> mockTreeService = new Mock<ITreeService>();
        public Mock<ILogger<TreeController>> mockLogger = new Mock<ILogger<TreeController>>();

        [Fact]
        public void GetLocations()
        {
            var trees = new List<Location>()
            {
                new Location(3.11, 108.11),
                new Location(3.12, 109.12)
            };

            mockTreeService.Setup(p => p.GetAllLocations()).Returns(trees);

            ILogger<TreeController> logger = mockLogger.Object;
            logger = Mock.Of<ILogger<TreeController>>();

            TreeController tree = new TreeController(logger, mockTreeService.Object);
            IEnumerable<Location> result = tree.ListLocations();
            Assert.True(trees.Equals(result));
        }

        [Theory, MemberData(nameof(GuidSingleLocation))]
        public void GetLocationById(double lat, double lng, Guid id)
        {
            var location = new Location(lat, lng);

            mockTreeService.Setup(p => p.GetLocationById(id)).Returns(location);

            ILogger<TreeController> logger = mockLogger.Object;
            logger = Mock.Of<ILogger<TreeController>>();

            TreeController tree = new TreeController(logger, mockTreeService.Object);
            Location result = tree.GetLocation(id);
            Assert.True(location.Equals(result));
        }

        public static IEnumerable<object[]> GuidSingleLocation
        {
            get
            {
                yield return new object[] { 3.12114, 101.299648, new Guid("c9014bc1-fb5c-46f8-a397-2ac1d3326e99") };
            }
        }

    }
}
