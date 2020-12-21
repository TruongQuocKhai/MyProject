using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.CommonS
{
    [Serializable]
    public class UserSignIn
    {
        public long UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string GroupID { get; set; }
    }
}