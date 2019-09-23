using System.Collections.Generic;

namespace Bread2Bun.Common.Model
{
    public class PageList<T>
    {
        public long TimeTaken { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalRecordCount { get; set; }
        public PageList()
        {
            Skip = 0;
            Take = 0;
            TotalRecordCount = 0;
        }

        public PageList(IEnumerable<T> items, int skip, int take, int recordCount)
        {
            Items = items;
            Skip = skip;
            Take = take;
            TotalRecordCount = recordCount;
        }
    }
}
