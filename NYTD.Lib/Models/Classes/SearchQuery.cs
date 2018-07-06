using Google.Apis.YouTube.v3;

namespace NYTD.Lib
{
    public class SearchQuery : Query
    {
        public string Query { get; set; }
        public string Type { get; set; }
        public SearchResource.ListRequest.OrderEnum Order { get; set; } = SearchResource.ListRequest.OrderEnum.Relevance;
    }
}
