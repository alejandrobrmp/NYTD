using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using NYTD.Lib.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.Lib.Services.Interfaces
{
    public interface IYouTubeApiService
    {
        Task<SearchListResponse> Search(SearchQuery query);
    }
}
