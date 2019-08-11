using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.ExceptionHandler
{
    public class ExceptionHandlerBase
    {
        private static string errorMessage;
        public static string HandleException(Exception exception)
        {
            var exType = exception.GetType().Name;

            switch (exType)
            {
                case "ArgumentException": errorMessage = ArgumentExceptionHandler(exception); break;
            }

            return errorMessage;
        }
        public static string ArgumentExceptionHandler(Exception exception)
        {
            return exception.Message;

        }
    }


}
