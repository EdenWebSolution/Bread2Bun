using System.Collections.Generic;

namespace Bread2Bun.Common
{
    public class PaginationModel<T> where T : class
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Details { get; set; }
        public dynamic ExtensionData { get; set; }
    }
}
