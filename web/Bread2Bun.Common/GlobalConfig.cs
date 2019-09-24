using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Common
{
    public static class GlobalConfig
    {
        // development
        //public static string APIBaseUrl = "http://localhost:54969";
        //public static string PresentationBaseUrl = "http://localhost:4200";

        // production
        public static string APIBaseUrl = "https://www.bread2bun.com";
        public static string PresentationBaseUrl = "https://www.bread2bun.com";

        //// test
        //public static string APIBaseUrl = "http://test.bread2bun.com";
        //public static string PresentationBaseUrl = "http://test.bread2bun.com";
    }
}
