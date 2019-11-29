using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData.Requests;
using PGTManagement.Gateway.PGTData.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData
{
    public interface ICampusClient
    {

    }

    public class CampusClient : ICampusClient
    {
        public string ApiEndPoint { get; private set; }


        public CampusClient(AppSetting configuration)
        {
            ApiEndPoint = configuration.PGTData + "/api/Campus";
        }

        public async Task<List<CampusResult>> GetAll()
        {
            try
            {
                string URlQuery = ApiEndPoint;

                var result = await WebClientOfT<List<CampusResult>>.GetAsync(URlQuery);
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
