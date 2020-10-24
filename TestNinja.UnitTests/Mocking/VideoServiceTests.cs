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
            service.FileReader = new MockFileReader();

            var result = service.ReadVideoTitle();

            Assert.Contains("error", result, StringComparison.OrdinalIgnoreCase);
        }
    }
}