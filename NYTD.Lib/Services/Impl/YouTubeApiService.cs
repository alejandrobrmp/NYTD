using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using NYTD.Lib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.Lib.Services.Impl
{
    public class YouTubeApiService : IYouTubeApiService
    {
        private YouTubeService _youtubeService;

        public YouTubeApiService()
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "NYTD",
                ApiKey = "AIzaSyA7WI4ceM3QE0xr3ujotmDjc292nxDKyVo"
            });
        }

        public async Task<SearchListResponse> Search(SearchQuery query)
        {
            SearchResource.ListRequest listRequest = _youtubeService.Search.List(query.Part);
            listRequest.Q = query.Query;
            listRequest.Order = query.Order;
            listRequest.PageToken = query.PageToken;
            listRequest.MaxResults = query.MaxResults;

            return await listRequest.ExecuteAsync();
        }
    }
}
