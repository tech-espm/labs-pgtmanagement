using System;
namespace PGTManagement.Common.WebClient
{
    public class WebClientOfTException : Exception
    {
        public WebClientOfTException()
        {

        }

        public WebClientOfTException(ObjectResult obj)
            : base("HttpRequest Error")
        {
            this.ErrorResult = obj;
        }

        public ObjectResult ErrorResult
        {
            get;
            set;
        }
    }
}
