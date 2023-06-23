using Newtonsoft.Json;
using System.Text;

namespace RestClient
{
    public class RestClient<T> where T : class
    {
        /// <summary>
        /// Object to return
        /// </summary>
        private WebServiceResponse<T> Response;
        private T ObjectResponse { set; get; }

        /// <summary>
        /// Consume Post Endpoints
        /// </summary>
        /// <param name="parameters">Json with T Data</param>
        /// <param name="url">Url to consume</param>
        /// <returns>Object expected by client</returns>
        public async Task<WebServiceResponse<T>> PostRequest(string parameters, string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromHours(2);
                    StringContent content = new StringContent(parameters, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ObjectResponse = JsonConvert.DeserializeObject<T>(apiResponse);
                    }
                }

                this.Response = new WebServiceResponse<T>()
                {
                    BusinessRulesOk= true,
                    Data= this.ObjectResponse,
                    ServerOk= true,
                    Token = "",
                    UserMessage= ""
                };
            }
            catch(Exception ex) 
            {
                this.Response = new WebServiceResponse<T>()
                {
                    BusinessRulesOk = true,
                    DeveloperMessage = new string[]
                    {
                        new string("Error consuming <" + url + "> with <" + parameters + ">"),
                        new string(ex.Message),
                        new string(ex.StackTrace),
                    } ,
                    ServerOk = false,
                    Token = "",
                    UserMessage = ""
                };
            }
            

            return this.Response;
        }
    }
}