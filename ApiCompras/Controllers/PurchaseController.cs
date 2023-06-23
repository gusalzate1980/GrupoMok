using Dto.Classes;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestClient;
using BusinessRules.Classes;
using Newtonsoft.Json;

namespace ApiCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : MokController
    {
        private SaleBr _saleBr { set; get; }
        public PurchaseController(IConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
            this._saleBr = new SaleBr(configuration);
        }

        [HttpPost("InsertSale")]
        public async Task<ActionResult<WebServiceResponse<WildCard>>> InsertSale(WebServiceRequest<SaleDto> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                await this._saleBr.InsertSale(request.Data);
                var a = new WebServiceResponse<WildCard>()
                {
                    Data = new WildCard()
                    {
                        Value = this._saleBr.Id.ToString()
                    },
                    BusinessRulesOk = this._saleBr.BusinessRuleOk,
                    ServerOk = this._saleBr.ServerOk,
                    UserMessage = this._saleBr.UserMessage,
                };

                if (!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._saleBr.DeveloperMessage),
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
