namespace Bread2Bun.Common.Constants
{
    public static class FolderPath
    {
        public static string ImagePath
        {
            get
            {


#if DEBUG
                return @"http://localhost:54969/";

#elif RELEASE
                return @"https://www.bread2bun.com/";

# else
                return @"http://test.bread2bun.com/";

#endif

            }
        }


        public const string ProfileImages = @"profileImages/";
        public const string Review = @"review/";
        public const string FoodImages = @"foods/";
    }
}
