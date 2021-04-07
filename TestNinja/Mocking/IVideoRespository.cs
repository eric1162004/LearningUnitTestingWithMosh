using System.Collections.Generic;

namespace TestNinja.Mocking
{
    public interface IVideoRespository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}