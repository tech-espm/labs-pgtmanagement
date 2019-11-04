using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData.Requests
{
    public class StudentRequest
    {
        public string StudentName { get; set; }
        public int StudentRA { get; set; }
        public int GroupID { get; set; }
        public int CampusID { get; set; }
    }
}
