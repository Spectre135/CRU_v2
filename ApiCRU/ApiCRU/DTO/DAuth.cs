using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCRU.DTO
{
    public class DAuth
    {
        public string SessionAuthToken { get; set; }
        public Boolean IsUserValidInAD { get; set; }
    }
}