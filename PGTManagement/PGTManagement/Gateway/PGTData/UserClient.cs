using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData.Requests;
using PGTManagement.Gateway.PGTData.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData
{
    public interface IUserClient
    {

    }

    public class UserClient : IUserClient
    {
        public string ApiEndPoint { get; private set; }


        public UserClient(AppSetting configuration)
        {
            ApiEndPoint = configuration.PGTData + "/api/User";
        }

        public async Task<List<UserResult>> GetAll()
        {
            try
            {
                string URlQuery = ApiEndPoint;

                var result = await WebClientOfT<List<UserResult>>.GetAsync(URlQuery);
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserResult>> Get(int UserID)
        {
            try
            {

                string URlQuery = ApiEndPoint + "?UserID=" + UserID;

                var result = await WebClientOfT<List<UserResult>>.GetAsync(URlQuery);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserResult> Post(UserRequest req)
        {
            try
            {
                var result = await WebClientOfT<UserResult>.PostJsonAsync(ApiEndPoint, req);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}