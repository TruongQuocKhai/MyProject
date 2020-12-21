using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.CommonS
{
    public static class CommonConstSession
    {
        public static string CartSession = "CartSession"; //key cart session

        public static string USER_SESSION = "USER_SESSION"; //Key user session

        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";

        public static string CurrentCulture { set; get; }
    }
}