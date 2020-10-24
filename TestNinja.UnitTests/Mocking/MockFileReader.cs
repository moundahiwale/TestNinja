using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    public class MockFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}