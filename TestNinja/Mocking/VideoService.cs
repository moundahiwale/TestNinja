using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader { get; set; }

        // Poor man's dependency injection :D i.e. in a real world app, this class might have a 
        // couple more dependencies. We do not want to repeat _fileReader = fileReader ?? new 
        // FileReader(); a few times  and also we do not want to make the params optional
        public VideoService(IFileReader fileReader = null)
        {
            // by doing this existing code doesn't have to be modified e.g. new VideoService() will stay as is.
            // So in prod, new FileReader() will be used and in tests we can pass a mock file reader
            _fileReader = fileReader ?? new FileReader();
        }
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }


        public class Video
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool IsProcessed { get; set; }
        }
    }
}