using BusinessRules.Classes;
using Dto;
using Dto.Classes;
using GenericDataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestClient;

namespace ApiCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : MokController
    {
        private ProductBr _productBr { set;get;}
        public ProductController(IConfiguration configuration):base(configuration)
        {
            this._configuration = configuration;
            this._productBr = new ProductBr(configuration);
        }
        [HttpPost("InsertProduct")]
        public ActionResult<WebServiceResponse<WildCard>> InsertProduct(WebServiceRequest<ProductDto> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                this._productBr.InsertProduct(request.Data);
                var a= new WebServiceResponse<WildCard>()
                {
                    Data = new WildCard()
                    {
                        Value = this._productBr.Id.ToString()
                    },
                    BusinessRulesOk = this._productBr.BusinessRuleOk,
                    ServerOk = this._productBr.ServerOk,
                    UserMessage= this._productBr.UserMessage,
                };

                if(!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._productBr.DeveloperMessage),
                    };
                }

                return a;
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("UpdateProduct")]
        public ActionResult<WebServiceResponse<WildCard>> UpdateProduct(WebServiceRequest<ProductDto> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                     request.Token,
                                     request.Timestamp,
                                     Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                this._productBr.UpdateProduct(request.Data);
                var a = new WebServiceResponse<WildCard>()
                {
                    Data = new WildCard()
                    {
                        Value = this._productBr.Id.ToString()
                    },
                    BusinessRulesOk = this._productBr.BusinessRuleOk,
                    ServerOk = this._productBr.ServerOk,
                    UserMessage = this._productBr.UserMessage,
                };

                if (!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._productBr.DeveloperMessage),
                    };
                }

                return a;
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("RateProduct")]
        public ActionResult<WebServiceResponse<WildCard>> RateProduct(WebServiceRequest<RateProductDto> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                this._productBr.RateProduct(request.Data);
                var a = new WebServiceResponse<WildCard>()
                {
                    Data = new WildCard()
                    {
                        Value = this._productBr.Id.ToString()
                    },
                    BusinessRulesOk = this._productBr.BusinessRuleOk,
                    ServerOk = this._productBr.ServerOk,
                    UserMessage = this._productBr.UserMessage,
                };

                if (!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._productBr.DeveloperMessage),
                    };
                }

                return a;
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("GetFilteredProducts")]
        public ActionResult<WebServiceResponse<PaginatedQuery<FilteredProductsDto>>> GetFilteredProducts(WebServiceRequest<PaginationDto<GetFilteredProductsDto>> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                this._productBr.GetFilteredProducts(request.Data);
                var a = new WebServiceResponse<PaginatedQuery<FilteredProductsDto>>()
                {
                    Data = (PaginatedQuery<FilteredProductsDto>)this._productBr.Data,
                    BusinessRulesOk = this._productBr.BusinessRuleOk,
                    ServerOk = this._productBr.ServerOk,
                    UserMessage = this._productBr.UserMessage,
                };

                if (!a.ServerOk)
                {
                    a.DeveloperMessage = new string[]
                    {
                        new string(this._productBr.DeveloperMessage),
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
