using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData.Requests
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public int UserRegister { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int UserTypeID { get; set; }
        public int CampusID { get; set; }
    }
}
