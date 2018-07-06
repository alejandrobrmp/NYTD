using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYTD.Lib
{
    public class YouTubeApiService : IYouTubeApiService
    {
        private YouTubeService _youtubeService;

        public void Initialize(string API_KEY)
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = this.GetType().ToString(),
                ApiKey = API_KEY,
            });
        }

        public async Task<SearchListResponse> Search(SearchQuery query)
        {
            SearchResource.ListRequest listRequest = _youtubeService.Search.List(query.Part);
            listRequest.Q = query.Query;
            listRequest.Order = query.Order;
            listRequest.PageToken = query.PageToken;
            listRequest.MaxResults = query.MaxResults;
            listRequest.Type = query.Type;

            return await listRequest.ExecuteAsync();
        }

        public async Task<VideoListResponse> GetVideoInfo(VideoQuery query)
        {
            VideosResource.ListRequest listRequest = _youtubeService.Videos.List(query.Part);
            listRequest.Id = query.Id;
            listRequest.MaxResults = query.MaxResults;

            return await listRequest.ExecuteAsync();
        }

        public async Task<ChannelListResponse> GetChannelInfo(ChannelQuery query)
        {
            ChannelsResource.ListRequest listRequest = _youtubeService.Channels.List(query.Part);
            listRequest.Id = query.Id;
            listRequest.MaxResults = query.MaxResults;

            return await listRequest.ExecuteAsync();
        }

        public async Task<PlaylistListResponse> GetPlaylistInfo(PlaylistQuery query)
        {
            PlaylistsResource.ListRequest listRequest = _youtubeService.Playlists.List(query.Part);
            listRequest.Id = query.Id;
            listRequest.MaxResults = query.MaxResults;

            return await listRequest.ExecuteAsync();
        }
    }
}
