using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hospitad.Application.Keys
{
    public static class Messages
    {
        #region Authentication
        public static string LoginFailed => "User Authentication Failed";
        public static string RegisteredUserLoginFailed => "User Registered but login failed. Please login again.";
        public static string RegisterFailed => "User Registration Failed";
        #endregion

        #region Organization
        public static string GetOrganizationsFailed => "Load organization list failed.";
        #endregion
    }
}
