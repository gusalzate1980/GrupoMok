using BusinessRules.Classes;
using Dto;
using Dto.Classes;
using Dto.Classes.Geography;
using GenericDataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestClient;

namespace ApiCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : MokController
    {
        private StoreBr _storeBr { set; get; }
        public StoreController(IConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
            this._storeBr = new StoreBr(configuration);
        }
        [HttpPost("GetStoresSellTheMostByMonth")]
        public ActionResult<WebServiceResponse<PivotDto>> GetStoresSellTheMostByMonth(WebServiceRequest<WildCard> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                try
                {
                    this._storeBr.GetStoresSellTheMostByMonth(Int32.Parse(request.Data.Value));
                    var a = new WebServiceResponse<PivotDto>()
                    {
                        Data = (PivotDto)this._storeBr.DataReturned,
                        BusinessRulesOk = this._storeBr.BusinessRuleOk,
                        ServerOk = this._storeBr.ServerOk,
                        UserMessage = this._storeBr.UserMessage,
                    };

                    if (!a.ServerOk)
                    {
                        a.DeveloperMessage = new string[]
                        {
                        new string(this._storeBr.DeveloperMessage),
                        };
                    }

                    return a;
                }
                catch(Exception e)
                {
                     var a = new WebServiceResponse<PivotDto>()
                    {
                        Data = new PivotDto(),
                        BusinessRulesOk = false,
                        ServerOk = false,
                        UserMessage = "Bad request",
                        DeveloperMessage = new string[] 
                        {
                            new string("Excepcion->"+e.Message)
                        }
                    };

                    return a;
                }
                
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("GetNearestStoresToUserLocation")]
        public ActionResult<WebServiceResponse<List<GetNearestStoresToUserLocationDto>>> GetNearestStoresToUserLocation(WebServiceRequest<PointDto> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                     request.Token,
                                     request.Timestamp,
                                     Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                this._storeBr.GetNearestStoresToUserLocation(request.Data);
                var a = new WebServiceResponse<List<GetNearestStoresToUserLocationDto>>()
                {
                    Data = (List<GetNearestStoresToUserLocationDto>)this._storeBr.DataReturned,
                    BusinessRulesOk = this._storeBr.BusinessRuleOk,
                    ServerOk = this._storeBr.ServerOk,
                    UserMessage = this._storeBr.UserMessage,
                };

                if (!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._storeBr.DeveloperMessage),
                    };
                }

                return a;
            }
            else
            {
                return NotFound();
            }

        }
    }
}