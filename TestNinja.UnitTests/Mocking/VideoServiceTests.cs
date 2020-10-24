using System;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class VideoServiceTests
    {
        [Fact]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService();

            var result = service.ReadVideoTitle(new MockFileReader());

            Assert.Contains("error", result, StringComparison.OrdinalIgnoreCase);
        }
    }
}