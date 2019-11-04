using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData.Requests;
using PGTManagement.Gateway.PGTData.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Gateway.PGTData
{
    public interface IStudentClient
    {

    }

    public class StudentClient : IStudentClient
    {
        public string ApiEndPoint { get; private set; }


        public StudentClient(AppSetting configuration)
        {
            ApiEndPoint = configuration.PGTData + "/api/Student";
        }

        public async Task<List<StudentResult>> Get(int StudentID)
        {
            try
            {

                string URlQuery = ApiEndPoint + "?StudentID=" + StudentID;

                var result = await WebClientOfT<List<StudentResult>>.GetAsync(URlQuery);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StudentResult> Post(StudentRequest req)
        {
            try
            {
                var result = await WebClientOfT<StudentResult>.PostJsonAsync(ApiEndPoint, req);
                return result;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
