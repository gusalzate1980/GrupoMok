using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Util;

namespace ApiNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MokController : ControllerBase
    {
        protected IConfiguration _configuration;

        public MokController(IConfiguration configuration) 
        {
            this._configuration= configuration;
        }
        protected bool ValidarPeticion(string jsonRequest,string token,int timestamp,int segundosValidesPeticion)
        {
            if (Encrypt.Sha256(jsonRequest).Equals(token))
            {
                int actualTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                try
                {
                    if (actualTimestamp - timestamp < segundosValidesPeticion)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}