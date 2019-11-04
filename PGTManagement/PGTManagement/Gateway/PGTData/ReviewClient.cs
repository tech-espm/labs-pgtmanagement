using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData.Requests;
using PGTManagement.Gateway.PGTData.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData
{
    public interface IReviewClient
    {

    }

    public class ReviewClient : IReviewClient
    {
        public string ApiEndPoint { get; private set; }


        public ReviewClient(AppSetting configuration)
        {
            ApiEndPoint = configuration.PGTData + "/api/Review";
        }

        public async Task<List<ReviewResult>> Get(int ReviewID)
        {
            try
            {

                string URlQuery = ApiEndPoint + "?ReviewID=" + ReviewID;

                var result = await WebClientOfT<List<ReviewResult>>.GetAsync(URlQuery);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReviewResult> Post(ReviewRequest req)
        {
            try
            {
                var result = await WebClientOfT<ReviewResult>.PostJsonAsync(ApiEndPoint, req);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
