using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Common.Constants
{
    public static class ExceptionType
    {
        public const string ArgumentException = "ArgumentException";
        public const string UnauthorizedAccessException = "UnauthorizedAccessException";


        #region DatabaseException

        public const string SqlException = "SqlException";

        #endregion
    }
}
