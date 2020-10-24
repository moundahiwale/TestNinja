using System.IO;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        public string ReadVideoTitle(IFileReader fileReader)
        {
            var str = fileReader.Read("video.txt");
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