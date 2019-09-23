namespace Bread2Bun.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }
    }
}
