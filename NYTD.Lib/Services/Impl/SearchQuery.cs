using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.Lib.Services.Impl
{
    public class SearchQuery
    {
        public string Part { get; set; }
        public string Query { get; set; }
        public long MaxResults { get; set; } = 10;
        public SearchResource.ListRequest.OrderEnum Order { get; set; }
        public string PageToken { get; set; }
    }
}
