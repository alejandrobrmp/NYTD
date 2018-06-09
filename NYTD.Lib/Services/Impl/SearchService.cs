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
    public class SearchService : IPagination<SearchResult, SearchListResponse>
    {
        private SearchQuery Query;
        private IYouTubeApiService service;

        public SearchService(IYouTubeApiService service, string query)
        {
            this.service = service;
            Query = new SearchQuery()
            {
                Part = "id",
                Query = query
            };
        }

        protected override async Task<(IList<SearchResult>, SearchListResponse)> Load()
        {
            Query.PageToken = LastRequest.NextPageToken ?? string.Empty;
            SearchListResponse response = await service.Search(Query);
            return (response.Items, response);
        }
    }
}
