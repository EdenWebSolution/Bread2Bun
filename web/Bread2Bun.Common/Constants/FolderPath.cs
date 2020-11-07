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
                return @"https://breadtobun.azurewebsites.net/";

# else
                return @"https://breadtobun.azurewebsites.net/";

#endif

            }
        }


        public const string ProfileImages = @"profileImages/";
        public const string Review = @"review/";
        public const string FoodImages = @"foods/";
    }
}
