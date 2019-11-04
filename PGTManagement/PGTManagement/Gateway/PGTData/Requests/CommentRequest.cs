using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData.Requests
{
    public class CommentRequest
    {
        public int CommentID { get; set; }
        public string CommentDescription { get; set; }

        public int ReviewID { get; set; }
    }
}
