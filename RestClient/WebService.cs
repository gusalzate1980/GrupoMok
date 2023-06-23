using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    public class WebServiceRequest<T> where T:class
    {
        /// <summary>
        /// Dto for Api procesing
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Sha256 Value for T json string 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Seconds since 1970-01-01 for UTC time when Request is generated
        /// </summary>
        public  int Timestamp { get; set; }
    }

    public class WebServiceResponse<T> where T:class
    {
        /// <summary>
        /// Dto expected by the client
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Sha256 for Data Json String
        /// </summary>
        public string   Token { get; set; }

        /// <summary>
        /// Message to be shown for the final user
        /// </summary>
        public string   UserMessage { get; set; }

        /// <summary>
        /// Technical message to be shown to developers in execution error cases.
        /// In success it goes blank
        /// </summary>
        public string[] DeveloperMessage { get; set; }

        /// <summary>
        /// false If server process fails 
        /// </summary>
        public bool ServerOk { get; set; }

        /// <summary>
        /// false if all business roles are not met, even server doesnt fail.
        /// </summary>
        public bool BusinessRulesOk { get; set; }
    }
}