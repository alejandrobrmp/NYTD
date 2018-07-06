using Google.Apis.YouTube.v3.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYTD.Lib
{
    public interface IYouTubeApiService
    {
        void Initialize(string API_KEY);
        Task<SearchListResponse> Search(SearchQuery query);
        Task<VideoListResponse> GetVideoInfo(VideoQuery query);
        Task<ChannelListResponse> GetChannelInfo(ChannelQuery query);
        Task<PlaylistListResponse> GetPlaylistInfo(PlaylistQuery query);
    }
}
