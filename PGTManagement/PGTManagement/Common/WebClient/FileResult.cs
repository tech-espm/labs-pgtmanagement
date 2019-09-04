using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Common.WebClient
{
    public class FileResult
    {
        public byte[] Bytes
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
    }
}
