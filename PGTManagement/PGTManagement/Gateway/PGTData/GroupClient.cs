using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData.Requests;
using PGTManagement.Gateway.PGTData.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData
{
    public interface IGroupClient
    {

    }

    public class GroupClient : IGroupClient
    {
        public string ApiEndPoint { get; private set; }


        public GroupClient(AppSetting configuration)
        {
            ApiEndPoint = configuration.PGTData + "/api/Group";
        }

        public async Task<List<GroupResult>> GetAll()
        {
            try
            {
                string URlQuery = ApiEndPoint;

                var result = await WebClientOfT<List<GroupResult>>.GetAsync(URlQuery);
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GroupResult>> Get(int GroupID)
        {
            try
            {

                string URlQuery = ApiEndPoint + "?GroupID=" + GroupID;

                var result = await WebClientOfT<List<GroupResult>>.GetAsync(URlQuery);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GroupResult> Post(GroupRequest req)
        {
            try
            {
                var result = await WebClientOfT<GroupResult>.PostJsonAsync(ApiEndPoint, req);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
