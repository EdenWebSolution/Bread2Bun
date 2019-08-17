using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Common.Constants
{
    public struct ExceptionType
    {
        public const string ArgumentException = "ArgumentException";
        public const string UnauthorizedAccessException = "UnauthorizedAccessException";
        public const string AuthenticationException = "AuthenticationException";

        #region DatabaseException

        public const string SqlException = "SqlException";

        #endregion
    }
}
