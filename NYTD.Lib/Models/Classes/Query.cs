namespace NYTD.Lib
{
    public abstract class Query
    {
        public string Part { get; set; }
        public long MaxResults { get; set; } = 10;
        public string PageToken { get; set; }

    }
}
