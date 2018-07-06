using Google.Apis.YouTube.v3.Data;
using NYTD.App.Models;
using NYTD.App.Services.Interfaces;
using NYTD.Lib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYTD.App.Services.Impl
{
    public class SearchService : IPagination<SearchResult, SearchListResponse>, ISearchService
    {
        private SearchQuery Query;
        private IYouTubeApiService service;

        public SearchService(IYouTubeApiService service)
        {
            this.service = service;
        }

        public void SetSearchQuery(string query)
        {
            Query = new SearchQuery()
            {
                Part = "id,snippet",
                Query = query,
                MaxResults = 10
            };
        }

        public void Reset()
        {
            base.ResetRequest();
        }

        public async Task<SearchResponse> GetResults()
        {
            SearchResponse response = new SearchResponse();
            List<SearchResult> result = await LoadItems();

            List<string> videosId = new List<string>();
            List<string> channelsId = new List<string>();
            List<string> playlistsId = new List<string>();

            result.ForEach(item =>
            {
                switch (item.Id.Kind)
                {
                    case "youtube#video":
                        videosId.Add(item.Id.VideoId);
                        break;
                    case "youtube#channel":
                        channelsId.Add(item.Id.ChannelId);
                        break;
                    case "youtube#playlist":
                        playlistsId.Add(item.Id.PlaylistId);
                        break;
                }
            });

            VideoQuery videoQuery = new VideoQuery
            {
                Part = "snippet,contentDetails",
                Id = string.Join(",", videosId),
                MaxResults = this.Query.MaxResults
            };
            VideoListResponse videoResponse = await service.GetVideoInfo(videoQuery);
            response.Videos.AddRange(videoResponse.Items);

            ChannelQuery channelQuery = new ChannelQuery
            {
                Part = "snippet,statistics",
                Id = string.Join(",", channelsId),
                MaxResults = this.Query.MaxResults
            };
            ChannelListResponse channelResponse = await service.GetChannelInfo(channelQuery);
            response.Channels.AddRange(channelResponse.Items);

            PlaylistQuery playlistQuery = new PlaylistQuery
            {
                Part = "snippet,status,contentDetails",
                Id = string.Join(",", playlistsId),
                MaxResults = this.Query.MaxResults
            };
            PlaylistListResponse playlistResponse = await service.GetPlaylistInfo(playlistQuery);
            response.Playlists.AddRange(playlistResponse.Items);

            return response;
        }

        protected override async Task<(IList<SearchResult>, SearchListResponse)> Load()
        {
            Query.PageToken = LastRequest?.NextPageToken ?? string.Empty;
            SearchListResponse response = await service.Search(Query);
            return (response.Items, response);
        }
    }
}
