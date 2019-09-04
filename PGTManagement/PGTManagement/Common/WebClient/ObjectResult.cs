using System;
using System.Collections.Generic;

namespace PGTManagement.Common.WebClient
{
    public class ObjectResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public object Result { get; set; }

        public List<string> Errors { get; set; }
    }
}
