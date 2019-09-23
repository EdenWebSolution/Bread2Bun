using System.Collections.Generic;

namespace Bread2Bun.Common.Extensions
{
    public static class ArrayExtention
    {
        public static bool IsNullOrZero<T>(this List<T> arr)
        {
            if (arr.IsNull() || arr.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
