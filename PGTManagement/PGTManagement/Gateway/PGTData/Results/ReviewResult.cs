using PGTManagement.Gateway.PGTData.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData.Results
{
    public class ReviewResult
    {
        public int ReviewID { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewRelevance { get; set; }
        public string ReviewResearch { get; set; }
        public string ReviewMemorial { get; set; }
        public string ReviewAccording { get; set; }

        public DateTime ReviewDate { get; set; }

        public int FileID { get; set; }

        public int ReviewTypeID { get; set; }

        public List<CommentRequest> Comments { get; set; }
    }
}
