using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData.Requests
{
    public class ReviewRequest
    {
        public string ReviewContent { get; set; }
        public string ReviewRelevance { get; set; }
        public string ReviewResearch { get; set; }
        public string ReviewMemorial { get; set; }
        public string ReviewAccording { get; set; }

        public DateTime ReviewDate { get; set; }

        public int FileID { get; set; }

        public int ReviewTypeID { get; set; }
    }
}
