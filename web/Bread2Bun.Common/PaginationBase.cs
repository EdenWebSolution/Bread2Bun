namespace Bread2Bun.Common
{
    public class PaginationBase
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public string SearchQuery { get; set; }
        public string OrderBy { get; set; }
    }
}
